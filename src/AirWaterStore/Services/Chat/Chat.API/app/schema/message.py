from datetime import datetime
from typing import Optional
from pydantic import BaseModel


class MessageResponse(BaseModel):
    chat_room_id: str
    customer_id: int
    content: str
    sent_at: datetime


class MessageCreateRequest(BaseModel):
    customer_id: int
    chat_room_id: str
    content: str

