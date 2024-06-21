using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Services
{
    public class ServicesRepository(PetHealthDBContext dBContext, ILogger<ServicesRepository> logger) : BaseRepository<Service>(dBContext, logger), IServicesRepository
    {
        private readonly PetHealthDBContext _dBContext = dBContext;
        private readonly ILogger<ServicesRepository> _logger = logger;

        public async Task<(IEnumerable<Service> Services, int TotalCount)> GetListService(string searchString, int  ?TypeId,int pageNumber = 1 , int pageSize = 10)
        {
            var query = _dBContext.Services.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            if (TypeId.HasValue) // Assuming TypeId is a nullable type
            {
                query = query.Where(s => s.TypeId == TypeId.Value);
            }

            var totalCount = await query.CountAsync();
            var services = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (services, totalCount);
        }
    }
}
