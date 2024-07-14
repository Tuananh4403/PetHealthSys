using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Doctors
{
    public interface IDoctorServices
    {
        Task<PaginatedApiResponse<Object>> GetListDoctorAsync(string? searchString, int pageNumber = 1, int pageSize = 10);
    }
}
