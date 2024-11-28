using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;

namespace ReserveBite.Api.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int CuisineId { get; set; }
        [ForeignKey(nameof(CuisineId))]
        [JsonIgnore]
        public Cuisine Cuisine { get; set; }
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
        public Menu Menu { get; set; }

    }
}
