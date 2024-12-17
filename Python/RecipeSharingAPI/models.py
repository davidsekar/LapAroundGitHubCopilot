from typing import Optional
from pydantic import BaseModel

class Recipe(BaseModel):
    id: Optional[int] = None
    title: str
    description: str
    ingredients: str
    preparation_steps: str
    cooking_time: int
    category: str