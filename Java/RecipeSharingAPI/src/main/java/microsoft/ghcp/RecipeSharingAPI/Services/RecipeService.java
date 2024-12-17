package microsoft.ghcp.RecipeSharingAPI.Services;

import microsoft.ghcp.RecipeSharingAPI.Models.Recipe;
import microsoft.ghcp.RecipeSharingAPI.Repositories.RecipeRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;

import java.util.List;
import java.util.Optional;


@Service
public class RecipeService {


    @PersistenceContext
    private EntityManager entityManager;

    @Autowired
    private RecipeRepository recipeRepository;

    public Recipe getRecipeById(Long id) {
        Optional<Recipe> recipe = recipeRepository.findById(id);
        return recipe.orElse(null);
    }

    public List<Recipe> getRecipes() {
        return recipeRepository.findAll();
    }

    public Recipe addRecipe(Recipe recipe) {
        return recipeRepository.save(recipe);
    }

    public Recipe updateRecipe(Long id, Recipe recipeDetails) {
        Optional<Recipe> optionalRecipe = recipeRepository.findById(id);
        if (optionalRecipe.isPresent()) {
            Recipe recipe = optionalRecipe.get();
            recipe.setCategory(recipeDetails.getCategory());
            recipe.setIngredients(recipeDetails.getIngredients());
            recipe.setCookingTime(recipeDetails.getCookingTime());
            return recipeRepository.save(recipe);
        } else {
            return null;
        }
    }

    public void deleteRecipe(Long id) {
        recipeRepository.deleteById(id);
    }

    public List<Recipe> findByTitle(String title){
        String jql = "from Recipe where title = '" + title + "'";  // its not typo, it is jql :)
        return entityManager.createQuery(jql, Recipe.class)
                            .getResultList();
        
    }
}