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
using PetCareSystem.Services.Services.Serivces;
using PetCareSystem.Data.Repositories.Staffs;
using System.Reflection.Metadata.Ecma335;
using PetCareSystem.Data.Repositories.Barns;

namespace PetCareSystem.Services.Services.Barns
{
    public class BarnService : IBarnService
    {
        private readonly IBarnRepository _barnRepository;
        public BarnService( IBarnRepository barnRepository)
        {
            _barnRepository = barnRepository;
        }

        public async Task<PaginatedApiResponse<object>> GetListBarn( int pageNumber = 1, int pageSize = 10)
        {
            var result = await _barnRepository.GetAllAsync();
            var totalCount =  result.Count();
            return new PaginatedApiResponse<object>(result,totalCount, pageNumber, pageSize);
        }
    }
    }