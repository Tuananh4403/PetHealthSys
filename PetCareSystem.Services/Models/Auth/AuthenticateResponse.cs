using PetCareSystem.Data.Entites;

namespace PetCareSystem.Services.Models.Auth
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<Role>? Role { get; set; }

        public AuthenticateResponse(User user,string token, List<Role> role)
        {
            Id = user.Id;
            FullName = user.FirstName + " " + user.LastName;
            Email = user.Email;
            Username = user.Username;
            Token = token;
            Role = role;
            
        }
    }
}
