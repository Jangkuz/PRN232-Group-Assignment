from fastapi import APIRouter
from app.routes import chatroom_endpoint

router = APIRouter()

router.include_router(chatroom_endpoint.router, prefix="/chatrooms", tags=["Chatrooms"])
