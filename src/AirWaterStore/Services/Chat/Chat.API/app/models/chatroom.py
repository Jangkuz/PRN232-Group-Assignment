from pydantic import BaseModel, Field
from app.core.types import PyObjectId

class ChatRoom(BaseModel):
    chat_room_id: PyObjectId = Field(default_factory=PyObjectId, alias="_id")
    customer_id: int
    staff_id: int | None = Field(default=None)

    class Settings:
        name = "chatrooms"