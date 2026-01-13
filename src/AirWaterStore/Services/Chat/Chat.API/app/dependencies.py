from fastapi import Depends
from app.core.database import db_manager
from app.core.config import settings
from app.services.chat_service import ChatRoomService


def get_db():
    return db_manager.client[settings.DATABASE_NAME]


async def get_chatroom_service(db=Depends(get_db)):
    return ChatRoomService(db)
