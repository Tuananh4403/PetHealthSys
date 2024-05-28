using Microsoft.Identity.Client;
using PetCareSystem.Data.Entites;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task RegisterAsync(string username, string password);
    }
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
