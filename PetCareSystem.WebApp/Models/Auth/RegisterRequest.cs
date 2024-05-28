using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.WebApp.Models.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
