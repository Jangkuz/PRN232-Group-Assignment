from pydantic import BaseModel, Field

class ChatRoom(BaseModel):
    id: str | None = Field(alias="_id")
    chat_room_id: int
    customer_id: int
    staff_id: int | None = Field(default=None)

    class Settings:
        name = "chatrooms"