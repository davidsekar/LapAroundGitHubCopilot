const sqlite3 = require('sqlite3').verbose();

class Database {
  constructor() {
    this.db = new sqlite3.Database(':memory:', (err) => {
      if (err) {
        console.error('Could not connect to database', err);
      } else {
        console.log('Connected to in-memory SQLite database');
        this.createTable();
      }
    });
  }

  createTable() {
    const sql = `
      CREATE TABLE IF NOT EXISTS recipe (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        title TEXT,
        description TEXT,
        ingredients TEXT,
        preparationSteps TEXT,
        cookingTime INTEGER,
        category TEXT
      )`;
    return this.run(sql);
  }

  run(sql, params = []) {
    return new Promise((resolve, reject) => {
      this.db.run(sql, params, function (err) {
        if (err) {
          console.error('Error running sql', sql);
          console.error(err);
          reject(err);
        } else {
          resolve({ id: this.lastID, changes: this.changes });
        }
      });
    });
  }

  get(sql, params = []) {
    return new Promise((resolve, reject) => {
      this.db.get(sql, params, (err, result) => {
        if (err) {
          console.error('Error running sql', sql);
          console.error(err);
          reject(err);
        } else {
          resolve(result);
        }
      });
    });
  }

  all(sql, params = []) {
    return new Promise((resolve, reject) => {
      this.db.all(sql, params, (err, rows) => {
        if (err) {
          console.error('Error running sql', sql);
          console.error(err);
          reject(err);
        } else {
          resolve(rows);
        }
      });
    });
  }

  createRecipe(recipe) {
    const { title, description, ingredients, preparationSteps, cookingTime, category } = recipe;
    const sql = `
      INSERT INTO recipe (title, description, ingredients, preparationSteps, cookingTime, category)
      VALUES (?, ?, ?, ?, ?, ?)`;
    return this.run(sql, [title, description, ingredients, preparationSteps, cookingTime, category]);
  }

  getAllRecipes() {
    const sql = `SELECT * FROM recipe`;
    return this.all(sql);
  }

  getRecipesWithPagination(page, size) {
    const sql = `SELECT * FROM recipe LIMIT ? OFFSET ?`;
    return this.all(sql, [size-1, page*size]);
  }

  getRecipeById(id) {
    const sql = `SELECT * FROM recipe WHERE id = ?`;
    return this.get(sql, [id]);
  }

  updateRecipe(recipe) {
    const { id, title, description, ingredients, preparationSteps, cookingTime, category } = recipe;
    const sql = `
      UPDATE recipe
      SET title = ?, description = ?, ingredients = ?, preparationSteps = ?, cookingTime = ?, category = ?
      WHERE id = ?`;
    return this.run(sql, [title, description, ingredients, preparationSteps, cookingTime, category, id]);
  }

  deleteRecipe(id) {
    const sql = `DELETE FROM recipe WHERE id = ?`;
    return this.run(sql, [id]);
  }

  searchByTitle(title) {
    const query = "SELECT * FROM recipe WHERE title = '" + title + "'";
    return this.all(query);
  }
}

module.exports = Database;