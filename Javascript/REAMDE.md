# Recipe Sharing API

This project is a Recipe Management API designed to help users efficiently manage and interact with a collection of recipes. 
It enables users to create, update, retrieve, search, and delete recipes, making it simple to organize and access culinary data. 
The API includes features such as searching for recipes by title, retrieving recipes in paginated format, and fetching detailed recipe information using a unique ID, streamlining recipe management for various use cases.

## Table of Contents

- [API Structure](#api-structure)
- [Gettings Started](#gettings-started)

### API Structure

## 1. Get All Recipes
- **Route**: `GET /api/recipes`
- **Description**: Retrieves a list of all recipes stored in the database.
- **Response**: Returns an array of Recipe objects.

## 2. Get Recipe by ID
- **Route**: `GET /api/recipes/{id}`
- **Description**: Retrieves a recipe by its unique ID.
- **URL Parameters**: 
  - `id` (integer): The ID of the recipe.
- **Response**: Returns the Recipe object if found, or a 404 error if not.

## 3. Search Recipes by Title
- **Route**: `GET /api/recipes/search`
- **Description**: Searches for recipes by title.
- **Query Parameters**: 
  - `title` (string): The title to search for.
- **Response**: Returns an array of matching Recipe objects, or a 204 No Content if no results are found.

## 4. Get Recipes with Pagination
- **Route**: `GET /api/recipes/page`
- **Description**: Retrieves a paginated list of recipes.
- **Query Parameters**:
  - `pageNumber` (integer): The page number to retrieve.
  - `pageSize` (integer): The number of recipes per page.
- **Response**: Returns a paginated list of Recipe objects, or a 204 No Content if no results are found.

## 5. Create a New Recipe
- **Route**: `POST /api/recipes`
- **Description**: Creates a new recipe.
- **Request Body**: A Recipe object containing the recipe details.
- **Response**: Returns the created Recipe object with a 201 status code, and a Location header pointing to the newly created recipe.

## 6. Update a Recipe
- **Route**: `PUT /api/recipes/{id}`
- **Description**: Updates an existing recipe by its ID.
- **URL Parameters**:
  - `id` (integer): The ID of the recipe to update.
- **Request Body**: The updated Recipe object.
- **Response**: Returns the updated Recipe object if successful, or a 404 error if the recipe is not found.

## 7. Delete a Recipe
- **Route**: `DELETE /api/recipes/{id}`
- **Description**: Deletes a recipe by its ID.
- **URL Parameters**:
  - `id` (integer): The ID of the recipe to delete.
- **Response**: Returns a 204 No Content if successful, or a 404 error if the recipe is not found.

### Gettings Started

1. **Install Dependencies**:
Ensure you have Node.js and npm installed. Then, install the required dependencies:
```bash
npm install
```

2. **Run the Application**:
Start the server:
```bash
npm start
```
3. **Access the API**:
The API will be accessible at http://localhost:3000 or another exposed port. You can interact with the endpoints using tools like Postman or cURL.L.