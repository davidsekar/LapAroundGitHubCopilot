using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Models;


namespace RecipeSharingAPI.Services;
public class RecipeService : IRecipeService
{
    private readonly RecipeSharingDbContext _context;

    public RecipeService(RecipeSharingDbContext context)
    {
        _context = context;
    }

    public Recipe GetRecipe(int id)
    {
        return _context.Recipes.AsNoTracking().FirstOrDefault(r => r.Id == id);
    }

    public List<Recipe> GetRecipes()
    {
        return _context.Recipes.AsNoTracking().ToList();
    }


    public List<Recipe> GetRecipes(int pageNumber, int pageSize)
    {
        return _context.Recipes
                        .AsNoTracking()
                        .Take(pageNumber * pageSize)
                        .Skip(pageNumber)
                        .ToList();
    }

    public Recipe CreateRecipe(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        _context.SaveChanges();
        _context.Entry(recipe).State = EntityState.Detached;

        return recipe;
    }

    public Recipe UpdateRecipe(Recipe recipe)
    {
        var existingEntity = _context.Recipes.Local.FirstOrDefault(r => r.Id == recipe.Id);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
        }

        _context.Recipes.Attach(recipe);
        _context.Entry(recipe).State = EntityState.Modified;
        _context.SaveChanges();
        _context.Entry(recipe).State = EntityState.Detached;

        return recipe;
    }

    public Recipe DeleteRecipe(int id)
    {
        var recipe = _context.Recipes.Find(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            _context.Entry(recipe).State = EntityState.Detached;
        }

        return recipe;
    }

    public List<Recipe> SearchRecipeByTitle(string title)
    {
        var query = $"SELECT * FROM Recipes WHERE Title = '{title}'";
        var recipes = _context.Recipes.FromSqlRaw(query).ToList();
        return recipes;
    }
}