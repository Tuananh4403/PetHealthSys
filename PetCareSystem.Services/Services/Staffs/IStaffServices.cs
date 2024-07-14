using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Enums;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Serivces
{
    public interface IStaffService 
    {
        Task<PaginatedApiResponse<Object>> GetListStaff(string? searchOption, int pageNumber = 1, int pageSize = 10);
    }
}
