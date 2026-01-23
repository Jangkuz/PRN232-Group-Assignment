from beanie import Document
from typing import Optional


class ChatRoom(Document):
    # chat_room_id: PyObjectId = Field(default_factory=PyObjectId, alias="_id")
    customer_id: int
    staff_id: Optional[int] = None

    class Settings:
        name = "chatrooms"
        indexes = [
            "customer_id",
            "staff_id",
        ]
