from pydantic import BaseModel
from datetime import datetime
from typing import Optional

class MessageCreate(BaseModel):
    chat_room_id: int
    user_id: int
    content: str

class MessageOut(BaseModel):
    message_id: int
    chat_room_id: int
    user_id: int
    content: str
    sent_at: datetime
    username: str

class ChatRoomOut(BaseModel):
    chat_room_id: int
    customer_id: int
    staff_id: Optional[int]
