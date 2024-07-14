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
        public async Task<PaginatedApiResponse<Object>> GetListDoctorAsync(string? searchString, int pageNumber = 1, int pageSize = 10)
        {
               var (doctors, totalCount) = await _doctorRepository.GetListDoctor(searchString);
            if (!doctors.Any())
            {
                return new PaginatedApiResponse<Object>("No Doctor found", true);
            }
            var listDoctor = doctors.Select(async doctor => new
            {
                doctor.Id,
                name = doctor.User.FirstName + " " + doctor.User.LastName,
                doctor.Specialty,
                email = doctor.User.Email,
                phone = doctor.User.PhoneNumber,
                doctor.Status
            }).ToList();
             var result = await Task.WhenAll(listDoctor);

            return new PaginatedApiResponse<Object>(result, totalCount, pageNumber, pageSize);
        }
    }
}
