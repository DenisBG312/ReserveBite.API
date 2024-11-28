using System.ComponentModel.DataAnnotations;

namespace ReserveBite.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;  
        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
