from typing import List, Optional
from app.models import Message


class MessageRepository:
    async def create(self, message: Message) -> Message:
        """
        Creates a new message in the database.
        """
        await message.insert()
        return message

    async def get_by_id(self, message_id: int) -> Optional[Message]:
        """
        Retrieves a message by its custom integer message_id.
        """
        return await Message.find_one(Message.message_id == message_id)

    async def get_by_chat_room(
        self, chat_room_id: int, skip: int = 0, limit: int = 50
    ) -> List[Message]:
        """
        Retrieves messages for a specific chat room, sorted chronologically.
        """
        return (
            await Message.find(Message.chat_room_id == chat_room_id)
            .sort(+Message.sent_at)
            .skip(skip)
            .limit(limit)
            .to_list()
        )

    async def update(self, message_id: int, content: str) -> Optional[Message]:
        """
        Updates the content of a message.
        """
        message = await self.get_by_id(message_id)
        if message:
            message.content = content
            await message.save()
            return message
        return None

    async def delete(self, message_id: int) -> bool:
        """
        Deletes a message by its message_id.
        """
        message = await self.get_by_id(message_id)
        if message:
            await message.delete()
            return True
        return False


message_repo = MessageRepository()
