using Microsoft.Identity.Client;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Auth;
public interface IAuthService
    {
        Task<User?> GetById(int userId);
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task RegisterAsync(string username, string password);
        Task<bool> RegisterPetAsync(PetRequest model);
}
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
