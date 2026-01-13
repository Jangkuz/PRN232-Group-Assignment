from pydantic import BaseModel, Field

class User(BaseModel):
    id: str | None = Field(alias = "_id")
    user_id: int
    username: str
    role: int

    class Settings:
        name = "users"
