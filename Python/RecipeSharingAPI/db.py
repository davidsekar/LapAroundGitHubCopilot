import sqlite3
from sqlite3 import Error

from models import Recipe

class Database:
    def __init__(self):
        self.conn = None

    def connect(self):
        try:
            self.conn = sqlite3.connect(':memory:', check_same_thread=False)
            self.conn.row_factory = sqlite3.Row
            print("Connection to SQLite DB successful")
        except Error as e:
            print(f"The error '{e}' occurred")

    def create(self):
        create_table_query = """
        CREATE TABLE IF NOT EXISTS Recipe (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            description TEXT,
            ingredients TEXT,
            preparation_steps TEXT,
            cooking_time INTEGER,
            category TEXT
        );
        """
        try:
            cursor = self.conn.cursor()
            cursor.execute(create_table_query)
            self.conn.commit()
            print("Recipe table created successfully")
        except Error as e:
            print(f"The error '{e}' occurred")

    def get_all_recipes(self):
        select_query = "SELECT * FROM Recipe"
        cursor = self.conn.cursor()
        cursor.execute(select_query)
        recipes = cursor.fetchall()
        return [Recipe.parse_obj(self._row_to_dict(recipe)) for recipe in recipes]

    def get_one_recipe(self, id):
        select_query = "SELECT * FROM Recipe WHERE id = ?"
        cursor = self.conn.cursor()
        cursor.execute(select_query, (id,))
        recipe = cursor.fetchone()
        return Recipe.parse_obj(self._row_to_dict(recipe)) if recipe else None
    
    def find_by_title(self, title):
        cursor = self.conn.cursor()
        query = f"SELECT * FROM recipe WHERE title = '{title}'"
        cursor.execute(query)
        recipes = cursor.fetchall()
        return [Recipe.parse_obj(self._row_to_dict(recipe)) for recipe in recipes]


    def insert_recipe(self, recipe):
        insert_query = """
        INSERT INTO Recipe (title, description, ingredients, preparation_steps, cooking_time, category)
        VALUES (?, ?, ?, ?, ?, ?)
        """
        cursor = self.conn.cursor()
        cursor.execute(insert_query, (recipe['title'], recipe['description'], recipe['ingredients'], recipe['preparation_steps'], recipe['cooking_time'], recipe['category']))
        self.conn.commit()
        return cursor.lastrowid

    def update_recipe(self, recipe):
        update_query = """
        UPDATE Recipe
        SET title = ?, description = ?, ingredients = ?, preparation_steps = ?, cooking_time = ?, category = ?
        WHERE id = ?
        """
        cursor = self.conn.cursor()
        cursor.execute(update_query, (recipe['title'], recipe['description'], recipe['ingredients'], recipe['preparation_steps'], recipe['cooking_time'], recipe['category'], recipe['id']))
        self.conn.commit()
        return cursor.rowcount

    def delete_recipe(self, id):
        delete_query = "DELETE FROM Recipe WHERE id = ?"
        cursor = self.conn.cursor()
        cursor.execute(delete_query, (id,))
        self.conn.commit()
        return cursor.rowcount
    
            
    def _row_to_dict(self, row):
        return {
            "id": row[0],
            "title": row[1],
            "description": row[2],
            "ingredients": row[3],
            "preparation_steps": row[4],
            "cooking_time": row[5],
            "category": row[6]
        }