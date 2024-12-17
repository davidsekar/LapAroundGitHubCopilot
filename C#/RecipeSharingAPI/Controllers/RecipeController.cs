using Microsoft.AspNetCore.Mvc;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSharingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<Recipe>> GetRecipes()
        {
            var recipes = _recipeService.GetRecipes();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipeById(int id)
        {
            var recipe =  _recipeService.GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Recipe>> SearchRecipeByTitle(string title)
        {
            var recipes = _recipeService.SearchRecipeByTitle(title);

            if (recipes.Count == 0)
            {
                return NoContent();
            }

            return Ok(recipes);
        }

        [HttpPost]
        public ActionResult<Recipe> CreateRecipe([FromBody] Recipe recipe)
        {
            _recipeService.CreateRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe([FromBody] Recipe recipe)
        {
           
            var existingRecipe = _recipeService.GetRecipe(recipe.Id);
            
            if (existingRecipe == null)
            {
                return NotFound();
            }

            var updatedRecipe = _recipeService.UpdateRecipe(recipe);
            return Ok(updatedRecipe);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var recipe = _recipeService.GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _recipeService.DeleteRecipe(id);
            return NoContent();
        }
    }
}