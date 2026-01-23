from typing import Optional, Annotated
from datetime import datetime, timezone
from beanie import Document, Indexed
from pydantic import Field


class User(Document):
    user_id: Annotated[int,Indexed(unique=True)]
    username: str
    role: int

    class Settings:
        name = "users"


class ChatRoom(Document):
    # chat_room_id: Annotated[int,Indexed(unique=True)]
    customer_id: int
    staff_id: Optional[int] = None

    class Settings:
        name = "chatrooms"
        indexes = [
            "customer_id",
            "staff_id"
        ]


class Message(Document):
    # message_id: Annotated[int,Indexed(unique=True)]
    chat_room_id: str
    user_id: int
    content: str
    sent_at: datetime = Field(default_factory=lambda: datetime.now(timezone.utc))

    class Settings:
        name = "messages"
        indexes = [
            "chat_room_id",
            "user_id"
        ]
