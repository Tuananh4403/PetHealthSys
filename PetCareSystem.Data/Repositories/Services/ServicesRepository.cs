using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Services
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly PetHealthDBContext _dBContext;

        public ServicesRepository(PetHealthDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<bool> AddServiceAsync(Service service)
        {
            await _dBContext.Services.AddAsync(service);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Log or handle the exception as needed
                return false;
            }
        }
    }
}
