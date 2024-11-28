using System.ComponentModel.DataAnnotations;

namespace ReserveBite.Api.Models
{
    public class RestaurantCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CuisineId { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public double Rating { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;
    }
}
