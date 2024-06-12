using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Services
{
    public interface IServicesRepository
    {
        Task<bool> AddServiceAsync(Service service);
        Task<Boolean> SaveChangesAsync();
    }
}
