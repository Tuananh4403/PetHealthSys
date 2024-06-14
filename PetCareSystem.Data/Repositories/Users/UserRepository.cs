using PetCareSystem.Data.Entites;
using PetCareSystem.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace PetCareSystem.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public UserRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user, int? roleId)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Add User
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();

                    // Add UserRole if RoleId is provided
                    if (roleId.HasValue)
                    {
                        var userRole = new UserRole { UserId = user.Id, RoleId = roleId.Value };
                        user.UserRoles.Add(userRole);
                        await _dbContext.SaveChangesAsync();
                    }

                    // Commit transaction
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Rollback transaction if something went wrong
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task AddPetAsync(Pet pet)
        {
            await _dbContext.Pets.AddAsync(pet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> GetUserByPhone(string phone)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone);
        }
        public async Task<User> GetUserByEmail(string phone)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}