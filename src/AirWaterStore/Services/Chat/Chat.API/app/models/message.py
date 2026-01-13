from pydantic import BaseModel, Field
from datetime import datetime, timezone

class Message(BaseModel):
    id: str | None = Field(alias="_id")
    message_id: int
    chat_room_id: int
    user_id: int
    content: str
    sent_at: datetime = Field(default_factory=lambda: datetime.now(timezone.utc))

    class Settings:
        name = "messages"
