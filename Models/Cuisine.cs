using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReserveBite.Api.Models
{
    public class Cuisine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string ImgUrl { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}
