from typing import Optional
from pydantic import BaseModel


class ChatRoomResponse(BaseModel):
    chat_room_id: str
    customer_id: int
    staff_id: Optional[int] = None


class ChatRoomCreateRequest(BaseModel):
    customer_id: int


class ChatRoomUpdateStaffRequest(BaseModel):
    staff_id: int
