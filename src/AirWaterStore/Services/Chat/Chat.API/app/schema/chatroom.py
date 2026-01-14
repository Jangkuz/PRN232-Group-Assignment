from pydantic import BaseModel

class ChatRoomResponse(BaseModel):
    chat_room_id: str
    customer_id: int
    staff_id: int
