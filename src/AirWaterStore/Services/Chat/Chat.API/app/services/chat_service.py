from bson import ObjectId
from typing import List, Optional

from pymongo import AsyncMongoClient

class ChatRoomService:
    def __init__(self, db: AsyncMongoClient):
        self.db = db
        self.collection = self.db["chat_rooms"]

    async def get_or_create_chatroom(self, customer_id: int):
        # In Mongo, we can do 'upsert' to get or create in one atomic step
        # 'return_document=True' returns the document after the update/insert
        from pymongo import ReturnDocument

        chatroom = await self.collection.find_one_and_update(
            {"customer_id": customer_id},
            {"$setOnInsert": {"customer_id": customer_id, "staff_id": None, "created_at": "..."}},
            upsert=True,
            return_document=ReturnDocument.AFTER
        )
        return chatroom

    async def get_chatroom_by_id(self, chat_room_id: str):
        # Note: MongoDB IDs are usually strings (ObjectIds), not integers
        return await self.collection.find_one({"_id": ObjectId(chat_room_id)})

    async def get_chatrooms_by_user(self, user_id: int):
        # SQL OR is represented by the $or operator in MongoDB
        cursor = self.collection.find({
            "$or": [
                {"customer_id": user_id},
                {"staff_id": user_id}
            ]
        })
        return await cursor.to_list(length=100)

    async def assign_staff_to_chatroom(self, chat_room_id: str, staff_id: int):
        await self.collection.update_one(
            {"_id": ObjectId(chat_room_id)},
            {"$set": {"staff_id": staff_id}}
        )