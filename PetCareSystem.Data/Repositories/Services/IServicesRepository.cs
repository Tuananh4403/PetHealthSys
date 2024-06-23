using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Services
{
    public interface IServicesRepository : IRepository<Service>
    {
        Task<(IEnumerable<Service> Services, int TotalCount)> GetListService(string searchString, int ?TypeId, int pageNumber = 1, int pageSize = 10);
    }
}
