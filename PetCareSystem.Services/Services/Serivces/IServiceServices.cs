﻿using PetCareSystem.Data.Entites;
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
    public interface IServiceServices
    {
        Task<ApiResponse<string>> CreateServiceAsync(CreateServiceReq serviceReq);
        Task<PaginatedApiResponse<Service>> GetListServiceAsync(string? searchString, int typeId = 1, int pageNumber = 1, int pageSize = 10);
        object GetServiceByCategory(int id);
        Task<string> GetServiceCategoryNameAsync(int typeId);
    }
}
