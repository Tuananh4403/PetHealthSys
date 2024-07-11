using Microsoft.Extensions.Configuration;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Services;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Enums;
using PetCareSystem.Services.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareSystem.Data.Enums;

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
        public async Task<ApiResponse<string>> CreateServiceAsync(CreateServiceReq serviceReq)
        {
            try
            {
                ServiceUnit unit = (ServiceUnit)serviceReq.UnitId;
                var service = new Service()
                {
                    TypeId = serviceReq.TypeOfService,
                    Code = serviceReq.Code,
                    Name = serviceReq.Name,
                    Price = serviceReq.Price,
                    Unit = unit,
                    Note = serviceReq.Note
                };

                var result = await _servicesRepository.AddAsync(service);
                if (result)
                {
                    return new ApiResponse<string>(message: "Create Serivce success!", true);
                }
                return new ApiResponse<string>(message: "Create Service fails!", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error add ServiceServices: " + ex);
                return new ApiResponse<string>(message: "Can create service", false);
            }
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
        public async Task<string> GetServiceCategoryNameAsync(int typeId)
        {
            var category = GetServiceCategoryById(typeId);
            return category.ToString(); // Assuming you just need the name of the enum  
        }
        public async Task<PaginatedApiResponse<Service>> GetListServiceAsync(string? searchString, int? typeId, int pageNumber = 1, int pageSize = 10)
        {
            var (services, totalCount) = await _servicesRepository.GetListService(searchString, typeId, pageNumber, pageSize);
            if (!services.Any())
            {
                return new PaginatedApiResponse<Service>("No services found", true);
            }
            var servicesWithCategoryTasks = services.Select(async service => new
            {
                service.Id,
                service.Name,
                service.TypeId,
                CategoryName = (await Task.Run(() => GetServiceCategoryById(service.TypeId))).ToString()
            }).ToList();
            var servicesWithCategory = await Task.WhenAll(servicesWithCategoryTasks);
            return new PaginatedApiResponse<Service>(services, totalCount, pageNumber, pageSize);
        }
    }
}
