using PetCareSystem.Data.Entites;
using PetCareSystem.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetCareSystem.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public UserRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
