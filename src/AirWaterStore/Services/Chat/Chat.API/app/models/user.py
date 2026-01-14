from pydantic import BaseModel, Field
from app.core.types import PyObjectId


class User(BaseModel):
    id: PyObjectId = Field(default_factory=PyObjectId, alias="_id")
    user_id: int = Field(unique=True)
    username: str = Field(unique=True)
    role: int

    class Settings:
        name = "users"
