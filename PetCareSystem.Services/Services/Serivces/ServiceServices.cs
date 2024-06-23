using Microsoft.Extensions.Configuration;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Services;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Enums;
using PetCareSystem.Services.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Serivces
{
    public class ServiceServices : IServiceServices
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IConfiguration _configuration;
        public ServiceServices(IServicesRepository servicesRepository, IConfiguration configuration)
        {
            _servicesRepository = servicesRepository;
            _configuration = configuration;
        }
        public async Task<bool> CreateServiceAsync(CreateServiceReq serviceReq)
        {

            var service = new Service()
            {
                TypeId = serviceReq.TypeOfService,
                Code = serviceReq.Code,
                Name = serviceReq.Name,
                Price = serviceReq.Price,
                Status = "NEW",
                Note = serviceReq.Note
            };

            // Save the service entity to the database
            return await _servicesRepository.AddAsync(service);
        }
        public object GetServiceByCategory(int categoryId)
        {
            ServiceCategory category = GetServiceCategoryById(categoryId);
            string categoryName = category.ToString(); // Converts enum to string
            switch (category)
            {
                case ServiceCategory.General:
                    return new { Category = categoryName, Data = GetGeneralService() };
                case ServiceCategory.Medicine:
                    return new { Category = categoryName, Data = GetMedicine() };
                case ServiceCategory.Vaccine:
                    return new { Category = categoryName, Data = GetVaccine() };
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }
        }

        private object GetGeneralService()
        {
            // Implementation
            return new { Description = "General service " };
        }

        private object GetMedicine()
        {
            // Implementation
            return new { Description = "Medicine service" };
        }

        private object GetVaccine()
        {
            // Implementation
            return new { Description = "Vaccine service" };
        }
        public ServiceCategory GetServiceCategoryById(int id)
        {
            if (!Enum.IsDefined(typeof(ServiceCategory), id))
            {
                throw new ArgumentOutOfRangeException(nameof(id), id, "Invalid service category ID");
            }
            return (ServiceCategory)id;
        }
        public async Task<(IEnumerable<Service> Services, int TotalCount)> GetListServiceAsync(string searchString, int TypeId = 1, int pageNumber = 1, int pageSize = 10)
        {
            return await _servicesRepository.GetListService(searchString, TypeId, pageNumber, pageSize);
        }
    }
}
