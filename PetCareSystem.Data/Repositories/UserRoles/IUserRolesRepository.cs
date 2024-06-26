using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.UserRoles
{
    public interface IUserRolesRepository : IRepository<UserRole>
    {
        public Task<UserRole> GetByUserId(int id);
    }
}
