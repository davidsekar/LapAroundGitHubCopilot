const express = require('express');
const Database = require('./db');
const { Recipe } = require('./model');

const db = new Database();

const app = express();

app.use(express.json());

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});

app.get('/api/Recipe', async (req, res) => {
    try {
        const recipes = await db.getAllRecipes();
        res.status(200).json(recipes);
    } catch (error) {
        res.status(500).json({ error: 'Failed to get recipes' });
    }
});

app.get('/api/Recipe/:id', async (req, res) => {
    try {
        const recipe = await db.getRecipeById(req.params.id);
        if (recipe) {
            res.status(200).json(recipe);
        } else {
            res.status(404).json({ error: 'Recipe not found' });
        }
    } catch (error) {
        res.status(500).json({ error: 'Failed to get recipe' });
    }
});

app.get('/api/Recipe/search/:title', async (req, res) => {
    try {
        const recipes = await db.searchByTitle(req.params.title);
        if (recipes.length > 0) {
            res.status(200).json(recipes);
        } else {
            res.status(400).json({ error: 'No recipes found with that title' });
        }
    } catch (error) {
        res.status(500).json({ error: 'Failed to search recipes' });
    }
});

app.get('/api/Recipe/paginate', async (req, res) => {
    const page = parseInt(req.query.page) || 1;
    const size = parseInt(req.query.size) || 10;

    try {
        const recipes = await db.getRecipesWithPagination(page, size);
        res.status(200).json(recipes);
    } catch (error) {
        res.status(500).json({ error: 'Failed to get paginated recipes' });
    }
});

app.post('/api/Recipe', async (req, res) => {
    try {
        const newRecipe = await db.createRecipe(req.body);
        res.status(201).json(newRecipe);
    } catch (error) {
        res.status(500).json({ error: 'Failed to create recipe' });
    }
});

app.put('/api/Recipe', async (req, res) => {
    try {
        const updatedRecipe = await db.updateRecipe(req.body);
        if (updatedRecipe) {
            res.status(200).json(updatedRecipe);
        } else {
            res.status(404).json({ error: 'Recipe not found' });
        }
    } catch (error) {
        res.status(500).json({ error: 'Failed to update recipe' });
    }
});

app.delete('/api/Recipe/:id', async (req, res) => {
    try {
        const result = await db.deleteRecipe(req.params.id);
        if (result) {
            res.status(200).json({ message: 'Recipe deleted successfully' });
        } else {
            res.status(404).json({ error: 'Recipe not found' });
        }
    } catch (error) {
        res.status(500).json({ error: 'Failed to delete recipe' });
    }
});


