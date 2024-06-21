using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Enums;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Serivces
{
    public interface IServiceServices
    {
        Task<bool> CreateServiceAsync(CreateServiceReq serviceReq);
        Task<(IEnumerable<Service> Services, int TotalCount)> GetListServiceAsync(string searchString, int TypeId = 1, int pageNumber = 1, int pageSize = 10);
        object GetServiceByCategory(int id);
    }
}
