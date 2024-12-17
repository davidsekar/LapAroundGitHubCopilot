using Microsoft.EntityFrameworkCore;

namespace RecipeSharingAPI.Models
{
    public class RecipeSharingDbContext : DbContext
    {
        public RecipeSharingDbContext(DbContextOptions<RecipeSharingDbContext> options)
            : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}