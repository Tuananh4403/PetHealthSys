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

namespace PetCareSystem.Services.Services.Staffs
{
    public class StaffSservice : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffSservice( IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public async Task<PaginatedApiResponse<Object>> GetListStaff(string? searchOption,int pageNumber = 1, int pageSize = 10)
        {
            var (ListStaff, totalCount) = await _staffRepository.GetListStaff(searchOption);
            var resultStaff = ListStaff.Select(async staff => new
                        {
                            staff.Id,
                            fullName = staff.User.FirstName + " " +staff.User.LastName,
                            dob = staff.User.Birthday,
                            email = staff.User.Email,
                            phone = staff.User.PhoneNumber,

                        }).ToList();
                var result = await Task.WhenAll(resultStaff);
            return new PaginatedApiResponse<Object>(result, totalCount, pageNumber, pageSize);
        }
    }
}