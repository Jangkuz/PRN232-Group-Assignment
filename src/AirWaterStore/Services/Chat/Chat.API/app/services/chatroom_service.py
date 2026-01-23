from bson import ObjectId
from typing import List, Optional
from app.core.config import settings
from app.models.chatroom import ChatRoom
from pymongo import AsyncMongoClient
from pymongo.errors import DuplicateKeyError
# from pymongo.asynchronous.database import AsyncDatabase
# from pymongo.asynchronous.collection import AsyncCollection


class ChatRoomService:
    # def __init__(self, db: AsyncMongoClient):
    #     self.db: AsyncDatabase = db
    #     self.collection: AsyncCollection = self.db[settings.CHATROOM_COLLECTION_NAME]

    async def get_or_create_chatroom(self, customer_id: int) -> ChatRoom:
        existing = await self.collection.find_one({"customer_id": customer_id})

        if existing:
            return ChatRoom.model_validate(existing)

        chatRoom = ChatRoom(customer_id=customer_id, staff_id=None)

        doc = chatRoom.model_dump(by_alias=True, exclude={"chat_room_id"})

        try:
            result = await self.collection.insert_one(doc)
            chatRoom.chat_room_id = result.inserted_id
            return chatRoom

        except DuplicateKeyError as e:
            print(e.details)
            existing = await self.collection.find_one({"customer_id": customer_id})
            return ChatRoom.model_validate(existing)

    async def get_chatroom_by_id(self, chat_room_id: str) -> ChatRoom:
        doc = await self.collection.find_one({"_id": ObjectId(chat_room_id)})
        return ChatRoom.model_validate(doc)

    async def get_chatrooms_by_user(self, user_id: int):
        # SQL OR is represented by the $or operator in MongoDB
        cursor = self.collection.find(
            {"$or": [{"customer_id": user_id}, {"staff_id": user_id}]}
        )
        return await cursor.to_list(length=100)

    async def assign_staff_to_chatroom(self, chat_room_id: str, staff_id: int):
        await self.collection.update_one(
            {"_id": ObjectId(chat_room_id)}, {"$set": {"staff_id": staff_id}}
        )
