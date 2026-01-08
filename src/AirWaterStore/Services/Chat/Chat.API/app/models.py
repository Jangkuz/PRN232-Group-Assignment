from sqlalchemy import Column, Integer, String, DateTime, ForeignKey
from sqlalchemy.orm import relationship, declarative_base
from datetime import datetime

Base = declarative_base()

class User(Base):
    __tablename__ = "users"
    user_id = Column(Integer, primary_key=True, index=True)
    username = Column(String, nullable=False)
    role = Column(Integer, nullable=False)
    messages = relationship("Message", back_populates="user")

class ChatRoom(Base):
    __tablename__ = "chatrooms"
    chat_room_id = Column(Integer, primary_key=True, index=True)
    customer_id = Column(Integer, ForeignKey("users.user_id"))
    staff_id = Column(Integer, ForeignKey("users.user_id"), nullable=True)

    customer = relationship("User", foreign_keys=[customer_id])
    staff = relationship("User", foreign_keys=[staff_id])
    messages = relationship("Message", back_populates="chatroom")

class Message(Base):
    __tablename__ = "messages"
    message_id = Column(Integer, primary_key=True, index=True)
    chat_room_id = Column(Integer, ForeignKey("chatrooms.chat_room_id"))
    user_id = Column(Integer, ForeignKey("users.user_id"))
    content = Column(String, nullable=False)
    sent_at = Column(DateTime, default=datetime.now(datetime.timezone.utc))

    chatroom = relationship("ChatRoom", back_populates="messages")
    user = relationship("User", back_populates="messages")
