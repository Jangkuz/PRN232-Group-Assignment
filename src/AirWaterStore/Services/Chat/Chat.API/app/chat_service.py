from sqlalchemy.orm import Session
from . import models

class ChatRoomService:
    def __init__(self, db: Session):
        self.db = db

    def get_or_create_chatroom(self, customer_id: int):
        chatroom = self.db.query(models.ChatRoom).filter_by(customer_id=customer_id).first()
        if not chatroom:
            chatroom = models.ChatRoom(customer_id=customer_id)
            self.db.add(chatroom)
            self.db.commit()
            self.db.refresh(chatroom)
        return chatroom

    def get_chatroom_by_id(self, chat_room_id: int):
        return self.db.query(models.ChatRoom).filter_by(chat_room_id=chat_room_id).first()

    def get_chatrooms_by_user(self, user_id: int):
        return self.db.query(models.ChatRoom).filter(
            (models.ChatRoom.customer_id == user_id) |
            (models.ChatRoom.staff_id == user_id)
        ).all()

    def assign_staff_to_chatroom(self, chat_room_id: int, staff_id: int):
        chatroom = self.get_chatroom_by_id(chat_room_id)
        if chatroom:
            chatroom.staff_id = staff_id
            self.db.commit()
