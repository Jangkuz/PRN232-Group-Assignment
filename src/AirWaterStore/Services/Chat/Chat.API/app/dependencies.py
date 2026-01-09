from app.core.database import db_manager
from app.services.chat_service import ChatRoomService

async def get_chatroom_service():
    db = db_manager.get_db()

    return ChatRoomService(db)