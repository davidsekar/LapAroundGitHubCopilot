package microsoft.ghcp.RecipeSharingAPI.Repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import microsoft.ghcp.RecipeSharingAPI.Models.Recipe;

@Repository
public interface RecipeRepository extends JpaRepository<Recipe, Long> {
}