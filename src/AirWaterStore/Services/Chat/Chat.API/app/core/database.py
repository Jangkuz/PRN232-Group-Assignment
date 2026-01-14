from app.core.config import settings
from typing import Optional
from beanie import Document, Indexed, init_beanie
from pymongo import AsyncMongoClient
from fastapi import FastAPI
from loguru import logger


class Database:
    """
    MongoDB class to hold a single MongoClient instance for the application.
    """

    client: AsyncMongoClient = None


db_manager = Database()


async def mongodb_startup(app: FastAPI) -> None:
    """
    Establishes a connection to the MongoDB database on application startup.

    Args:
        app (FastAPI): The FastAPI application instance.

    This function sets the MongoDB client instance in the app state, allowing
    other parts of the application to access the MongoDB connection.
    """

    # TODO: if query and db is non-existence. Init db
    logger.info("Connecting to MongoDB...")
    mongo_client = AsyncMongoClient(settings.MONGODB_URL)
    db_manager.client = mongo_client
    await init_db(mongo_client)
    logger.info("Connected to MongoDB!")


async def mongodb_shutdown(app: FastAPI) -> None:
    """
    Closes the MongoDB connection on application shutdown.

    Args:
        app (FastAPI): The FastAPI application instance.

    Ensures that the MongoDB connection is gracefully closed when the application stops.
    """

    logger.info("Closing MongoDB connection...")
    db_manager.client.close()
    logger.info("MongoDb connection closed!")


async def init_db(client: AsyncMongoClient):
    db = client[settings.DATABASE_NAME]

    collection = db[settings.CHATROOM_COLLECTION_NAME]

    await collection.create_index("chat_room_id", unique=True)
    await collection.create_index("customer_id")
    await collection.create_index("staff_id")
