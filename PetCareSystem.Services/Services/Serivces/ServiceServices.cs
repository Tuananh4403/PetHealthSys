using Microsoft.Extensions.Configuration;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Services;
using PetCareSystem.Services.Models.Booking;
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
        private readonly Dictionary<int, string> _categoryMap;
        public ServiceServices(IServicesRepository servicesRepository, IConfiguration configuration )
        {
            _servicesRepository = servicesRepository;
            _configuration = configuration;

            _categoryMap = new Dictionary<int, string>
        {
            { 1, configuration["Category:Service:Name"] },
            { 2, configuration["Category:Medicine:Id"] },
            { 3, configuration["Category:Vaccine:Id"] }
        };
        }
        public async Task<bool> CreateServiceAsync(CreateServiceReq serviceReq)
        {

            var service = new Service()
            {
                TypeId      = serviceReq.TypeOfService,
                Code        = serviceReq.Code,
                Name        = serviceReq.Name,
                Price       = serviceReq.Price,
                Status      = "NEW",
                Note        = serviceReq.Note
            };

            // Save the service entity to the database
            return await _servicesRepository.AddServiceAsync(service);
        }
        public string GetCategoryName(int categoryId)
        {
            return _categoryMap.TryGetValue(categoryId, out var categoryName) ? categoryName : "Unknown";
        }
    }
}
