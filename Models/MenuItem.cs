using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveBite.Api.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; } = null!;
        [Required]
        public int MenuId { get; set; }
        [ForeignKey(nameof(MenuId))]
        public Menu Menu { get; set; }
    }
}
