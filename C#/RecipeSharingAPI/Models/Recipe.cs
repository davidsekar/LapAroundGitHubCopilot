using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeSharingAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public string? Ingredients { get; set; }

        [Required]
        public string? PreperationSteps { get; set; }

        [Required]
        public int CookingTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Category { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }
    }
}