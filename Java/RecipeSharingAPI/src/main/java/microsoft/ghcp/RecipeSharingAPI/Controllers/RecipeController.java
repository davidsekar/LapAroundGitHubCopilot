package microsoft.ghcp.RecipeSharingAPI.Controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import microsoft.ghcp.RecipeSharingAPI.Models.Recipe;
import microsoft.ghcp.RecipeSharingAPI.Services.RecipeService;
import java.util.List;

@RestController
@RequestMapping("/api/Recipe")
public class RecipeController {

    @Autowired
    private RecipeService recipeService;

    @GetMapping("/{id}")
    public ResponseEntity<Recipe> getRecipeById(@PathVariable Long id) {
        Recipe recipe = recipeService.getRecipeById(id);
        if (recipe != null) {
            return ResponseEntity.ok(recipe);
        } else {
            return ResponseEntity.noContent().build();
        }
    }

    @GetMapping
    public List<Recipe> getRecipes() {
        return recipeService.getRecipes();
    }

    @GetMapping("/search")
    public List<Recipe> findByTitle(@RequestParam String title) {
        return recipeService.findByTitle(title);
    }

    @GetMapping("/pagination")
    public Page<Recipe> getRecipesWithPagination(@RequestParam int page, @RequestParam int size) {

        Page<Recipe> recipes = recipeService.getRecipesWithPagination(size, page);

        if (recipes.isEmpty()) {
            return Page.empty();
        }

        return recipeService.getRecipesWithPagination(page, size);
    }

    @PostMapping
    public Recipe createRecipe(@RequestBody Recipe recipe) {
        return recipeService.addRecipe(recipe);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Recipe> updateRecipe(@PathVariable Long id, @RequestBody Recipe recipeDetails) {
        Recipe updatedRecipe = recipeService.updateRecipe(id, recipeDetails);
        if (updatedRecipe != null) {
            return ResponseEntity.ok(updatedRecipe);
        } else {
            return ResponseEntity.noContent().build();
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteRecipe(@PathVariable Long id) {
        Recipe recipe = recipeService.getRecipeById(id);

        if (recipe == null) {
            return ResponseEntity.noContent().build();
        }

        recipeService.deleteRecipe(id);

        return ResponseEntity.ok().build();
    }
}