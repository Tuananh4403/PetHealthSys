using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user, int? roleId);
        Task<User?> GetUserByPhone(string phone);
        Task<User?> GetUserByEmail(string email);
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserByPet(int  petId);
    }
}