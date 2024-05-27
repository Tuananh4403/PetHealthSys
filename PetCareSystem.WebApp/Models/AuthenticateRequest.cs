using System.ComponentModel.DataAnnotations;
namespace PetHealthSys.PetCareSystem.WebApp.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }
  
        [Required] 
        public string Password { get; set; }
    }
}
