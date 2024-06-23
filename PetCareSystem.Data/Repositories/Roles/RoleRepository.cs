using System.Security;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareSystem.Data.Repositories.Roles
{
    public class RoleRepository(PetHealthDBContext dBContext, ILogger<RoleRepository> logger) : BaseRepository<Role>(dBContext, logger), IRoleRepository
    {
        public async Task<Role> GetRoleByIdAsync(int RoleId)
        {
            return await dBContext.Roles.SingleOrDefaultAsync(u => u.Id == RoleId);
        }

        public async Task<Role> GetRoleByTitleAsync(string RoleTilte)
        {
            return await dBContext.Roles.SingleOrDefaultAsync(u => u.Title == RoleTilte);
        }
    }
}