from app.core.config import settings
from fastapi import FastAPI
from loguru import logger
from motor.motor_asyncio import AsyncIOMotorClient
from beanie import init_beanie
from app.models import __beanie_models__


async def mongodb_startup(app: FastAPI) -> None:
    """
    Establishes a connection to the MongoDB database on application startup.

    Args:
        app (FastAPI): The FastAPI application instance.

    This function sets the MongoDB client instance in the app state, allowing
    other parts of the application to access the MongoDB connection.
    """
    logger.info("Connecting to MongoDB...")

    client = AsyncIOMotorClient(settings.MONGODB_URL)
    await init_beanie(
        database=client[settings.DATABASE_NAME],
        document_models=__beanie_models__
        )

    logger.info("Connected to MongoDB!")