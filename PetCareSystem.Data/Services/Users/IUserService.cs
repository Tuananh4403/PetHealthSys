using Microsoft.AspNetCore.Identity.Data;

using PetCareSystem.WebApp.Models;
using PetCareSystem.Data.Entites;
using PetHealthSys.PetCareSystem.WebApp.Models;
namespace PetCareSystem.Data.Services.Users
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        /*void UpdateRole(int id, UpdateRequest model);*/
        void Delete(int id);
    }

}
