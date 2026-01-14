from pydantic import BaseModel, Field
from datetime import datetime, timezone
from app.core.types import PyObjectId

class Message(BaseModel):
    message_id: PyObjectId = Field(default_factory=PyObjectId, alias="_id")
    chat_room_id: str
    user_id: int
    content: str
    sent_at: datetime = Field(default_factory=lambda: datetime.now(timezone.utc))

    class Settings:
        name = "messages"
