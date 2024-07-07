using Newtonsoft.Json.Linq;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Doctors
{
    public class DoctorServices : IDoctorServices
    {
        IDoctorRepository _doctorRepository;
        public DoctorServices(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<PaginatedApiResponse<Doctor>> GetListDoctorAsync(string? searchString, int pageNumber = 1, int pageSize = 10)
        {
               var (doctors, totalCount) = await _doctorRepository.GetListDoctor(searchString);
            if (!doctors.Any())
            {
                return new PaginatedApiResponse<Doctor>("No Doctor found", true);
            }
            return new PaginatedApiResponse<Doctor>(doctors, totalCount);
        }
    }
}
