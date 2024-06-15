using System.Security;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetCareSystem.Data.Repositories.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PetHealthDBContext _dBContext;

        public RoleRepository(PetHealthDBContext dBContext){
            _dBContext = dBContext;
        }

        public async Task Create(Role role)
        {
            await _dBContext.Roles.AddAsync(role); 
            await _dBContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetRoleByIdAsync(int RoleId)
        {
            return await _dBContext.Roles.SingleOrDefaultAsync(u => u.Id == RoleId);
        }

        public async Task<Role> GetRoleByTitleAsync(string RoleTilte)
        {
            return await _dBContext.Roles.SingleOrDefaultAsync(u => u.Title == RoleTilte);
        }
    }
}