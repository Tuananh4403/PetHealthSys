using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Repositories.Barns
{
    public interface IBarnRepository : IRepository<Barn>
    {
        Task<bool> CheckRoleAsync(int? userId);
    }
}