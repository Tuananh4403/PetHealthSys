using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.UserRoles
{
    public class UserRolesRepository(PetHealthDBContext dbContext, ILogger<UserRolesRepository> logger) : BaseRepository<UserRole>(dbContext, logger), IUserRolesRepository
    {
        public async Task<UserRole> GetByUserId(int id)
        {
            return await dbContext.UserRoles.SingleOrDefaultAsync(u => u.UserId == id);
        }
    }
}
