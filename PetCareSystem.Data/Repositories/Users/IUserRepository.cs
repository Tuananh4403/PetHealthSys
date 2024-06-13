using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user, int? roleId);
        Task AddPetAsync(Pet pet);
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByPhone(string phone);
        Task<User?> GetUserByEmail(string email);
    }
}