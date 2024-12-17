from fastapi import FastAPI, HTTPException
from typing import List
from db import Database
from models import Recipe

app = FastAPI()

# Initialize the database
db = Database()
db.connect()
db.create()


@app.get("/api/Recipe", response_model=List[Recipe])
def get_all_recipes():
    recipes = db.get_all_recipes()
    return [recipe for recipe in recipes]

@app.get("/api/Recipe/{id}", response_model=Recipe)
def get_one_recipe(id: int):
    recipe = db.get_one_recipe(id)
    if recipe is None:
        raise HTTPException(status_code=404, detail="Recipe not found")
    return recipe

@app.post("/api/Recipe", response_model=int)
def insert_recipe(recipe: Recipe):
    recipe_data = recipe.dict(exclude={"id"})
    new_recipe_id = db.insert_recipe(recipe_data)
    return new_recipe_id

@app.put("/api/Recipe", response_model=int)
def update_recipe(recipe: Recipe):
    recipe_data = recipe.dict()
    updated_recipe_id = db.update_recipe(recipe_data)
    return updated_recipe_id

@app.delete("/api/Recipe/{id}")
def delete_recipe(id: int):
    recipe = db.get_one_recipe(id)
    if recipe is None:
        raise HTTPException(status_code=404, detail="Recipe not found")
    db.delete_recipe(id)
    return recipe