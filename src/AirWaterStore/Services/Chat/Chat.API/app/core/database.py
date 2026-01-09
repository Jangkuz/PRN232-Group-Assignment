import asyncio
from typing import Optional
from beanie import Document, Indexed, init_beanie
from pymongo import AsyncMongoClient
from app.core.config import settings

class Database:
    client: AsyncMongoClient = None

    def get_db(self):
        return self.client[settings.MONGODB_URL]

db_manager = Database()

