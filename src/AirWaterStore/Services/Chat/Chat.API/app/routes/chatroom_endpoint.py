from fastapi import APIRouter, Depends
from starlette.status import HTTP_200_OK
from typing import Annotated
from app.schema.chatroom import ChatRoomResponse
from app.services.chatroom_service import ChatRoomService
from app.dependencies import get_chatroom_service

router = APIRouter()


@router.get(
    "/chat-rooms/{chatRoomId}",
    status_code=HTTP_200_OK,
    response_description=" get chat room by id",
    name="chat_room: get_by_id",
    response_model_exclude=True,
    response_model=ChatRoomResponse,
)
async def get_chatroom_by_id(
    chatRoomId: str, service: ChatRoomService = Depends(get_chatroom_service)
):
    return await service.get_chatroom_by_id(chatRoomId)
