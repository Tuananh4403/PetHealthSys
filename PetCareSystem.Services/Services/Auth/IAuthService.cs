using Microsoft.Identity.Client;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Models.Auth;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Auth
{
    public interface IAuthService
    {
        Task<User?> GetById(int userId);
        Task<ApiResponse<AuthenticateResponse>> LoginAsync(string username, string password);
        Task<ApiResponse<string>> RegisterAsync(RegisterRequest model);
        Task CreateRole(CreateRoleReq model);
        Task<IEnumerable<Role>> GetListRole();
        Task<IEnumerable<User>> GetAll();
        Task<ApiResponse<string>> UpdateUserRole(UpdateUserRoleReq model);
    }
}
