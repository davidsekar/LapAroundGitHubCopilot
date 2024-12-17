class Recipe {
    constructor(id, title, description, ingredients, preparationSteps, cookingTime, category) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.ingredients = ingredients;
        this.preparationSteps = preparationSteps;
        this.cookingTime = cookingTime;
        this.category = category;
    }
}

module.exports = Recipe;