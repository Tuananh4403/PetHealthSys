using Microsoft.Identity.Client;
using PetCareSystem.Data.Entites;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Auth
{
    public interface IAuthService
    {
        Task<User?> GetById(int userId);
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task RegisterAsync(string username, string password, string firstName, string lastName, string email);
    }
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
