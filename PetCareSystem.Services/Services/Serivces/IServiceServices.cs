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
    }
}
