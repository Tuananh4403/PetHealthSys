using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.Staffs;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models.Barn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Barns
{

    public class BarnService : IBarnService
    {
        private readonly IBarnRepository _barnsRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IStaffRepository _staffRepository;
        public BarnService(IBarnRepository barnsRepository, IDoctorRepository doctorRepository,IStaffRepository staffRepository)
        {
            _barnsRepository = barnsRepository;
            _doctorRepository = doctorRepository;
            _staffRepository = staffRepository;
        }

        public async Task<bool> CreateBarnsAsync(BarnRequest barnRequest, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }
            else if (!await _doctorRepository.CheckRoleAsync(userId)|| await _staffRepository.CheckRoleAsync(userId))
                {
                throw new ArgumentException("Just doctor or staff can create barn");
            }
            var barn = new Barn()
            {
                DateStart = barnRequest.DateStart,
                DateEnd = barnRequest.DateEnd,
                Status = barnRequest.Status
            };
            if (!await _barnsRepository.AddAsync(barn))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateBarnsAsync(int barnId, BarnRequest barnRequest, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            int id = userId.Value;
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }
            else if (await _doctorRepository.GetByIdAsync(id) == null || await _staffRepository.GetByIdAsync(id) == null)
            {
                throw new ArgumentException("Just doctor or staff can create barn");
            }

            var barn = new Barn()
            {
                DateStart = barnRequest.DateStart,
                DateEnd = barnRequest.DateEnd,
                Status = barnRequest.Status
            };
            if (!await _barnsRepository.UpdateAsync(barn))
            {
                return false;
            }

            return false;
        }
    }
}
