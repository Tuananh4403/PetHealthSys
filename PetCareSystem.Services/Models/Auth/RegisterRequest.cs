using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.Services.Models.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public bool IsCustomer { get; set; }
        public string? Role { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
