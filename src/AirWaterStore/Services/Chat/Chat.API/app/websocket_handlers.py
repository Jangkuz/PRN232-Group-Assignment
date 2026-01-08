from fastapi import WebSocket, WebSocketDisconnect
from typing import Dict, List

class ConnectionManager:
    def __init__(self):
        self.active_connections: Dict[str, List[WebSocket]] = {}

    async def connect(self, websocket: WebSocket, group: str):
        await websocket.accept()
        self.active_connections.setdefault(group, []).append(websocket)

    def disconnect(self, websocket: WebSocket, group: str):
        self.active_connections[group].remove(websocket)

    async def broadcast(self, group: str, message: dict):
        for conn in self.active_connections.get(group, []):
            await conn.send_json(message)

manager = ConnectionManager()
    