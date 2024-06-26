using PetCareSystem.Data.Entites;
using PetCareSystem.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace PetCareSystem.Data.Repositories.Users
{
    public class UserRepository(PetHealthDBContext dbContext, ILogger<UserRepository> logger) : BaseRepository<User>(dbContext, logger), IUserRepository
    {

        public async Task AddUserAsync(User user, int? roleId)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Add User
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();

                    // Add UserRole if RoleId is provided
                    if (roleId.HasValue)
                    {
                        var userRole = new UserRole { UserId = user.Id, RoleId = roleId.Value };
                        user.UserRoles.Add(userRole);
                        await dbContext.SaveChangesAsync();
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

        public async Task<User> GetUserByPhone(string phone)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await dbContext.Users.Include(u => u.UserRoles)
                                            .ThenInclude(ur => ur.Role)
                                        .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await dbContext.Users.Include(u => u.UserRoles)
                                            .ThenInclude(ur => ur.Role)
                                        .SingleOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbSet.Include(u => u.UserRoles)
                                            .ThenInclude(ur => ur.Role)
                               .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}