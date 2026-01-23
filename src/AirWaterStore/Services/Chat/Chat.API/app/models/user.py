from beanie import Document, Indexed
from typing import Annotated


class User(Document):
    user_id: Annotated[int, Indexed(unique=True)]
    username: str
    role: int

    class Settings:
        name = "users"
