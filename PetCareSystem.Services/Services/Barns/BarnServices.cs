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
using PetCareSystem.Services.Helpers;
using System.Globalization;

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
            var result = await _barnRepository.FindAsync(b => b.Status == true);
            var totalCount =  result.Count();
            return new PaginatedApiResponse<object>(result,totalCount, pageNumber, pageSize);
        }
        public async Task<ApiResponse<string>> Create(string note , string? date)
        {
            var barn = new Barn{
                Note = note,
                Status = true,
                DateStart = DateTime.Now,
            };
            if(date != null)
            {
                var endDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                barn.DateEnd = endDate;
            }
            var result = await _barnRepository.AddAsync(barn); 
            return new ApiResponse<string>( "Create Barn success",!result);
        }
    }
    }