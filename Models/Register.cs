using System.ComponentModel.DataAnnotations;

namespace ReserveBite.Api.Models
{
    public class Register
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required] 
        public string Email { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
        [Required] 
        public string ConfirmPassword { get; set; } = null!;
    }
}
