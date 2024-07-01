using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Barns;
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
        public BarnService(IBarnRepository barnsRepository)
        {
            _barnsRepository = barnsRepository;
        }

        public async Task<bool> CreateBarnsAsync(BarnRequest barnRequest, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }
            else if (!await _barnsRepository.CheckRoleAsync(userId))
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
    }
}
