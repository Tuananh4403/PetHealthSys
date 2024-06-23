using System.Collections.Generic;
using System.Threading.Tasks;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Services.Models.Barn;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Services.Services.Barn
{
    public class BarnServices : IBarnServices
    {
        private readonly IBarnRepository _repository;

        public BarnServices(IBarnRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateBarnAsync(BarnReq barnReq)
        {
            var barn = new Data.Entites.Barn()
            {
                DateStart = barnReq.DateStart,
                DateEnd = barnReq.DateEnd,
                Status = barnReq.Status,
                Result = barnReq.Result
            };
            return await _repository.CreateBarnAsync(barn);
        }
        
        public async Task<bool> DeleteBarnAsync(int id)
        {
            return await _repository.DeleteBarnAsync(id);
        }

        public Task<IList<Data.Entites.Barn>> GetBarn()
        {
            return _repository.GetBarns();
        }

        public Task<IList<Data.Entites.Barn>> GetBarnStatusFalse(bool status)
        {
            return _repository.GetBarnsAsync(status);
        }

        public async Task<bool> UpdateBarnAsync(int id, BarnReq barnReq)
        {
            var barn = new Data.Entites.Barn
            {
                Id = id,
                DateStart = barnReq.DateStart,
                DateEnd = barnReq.DateEnd,
                Status = barnReq.Status,
                Result = barnReq.Result
            };
            return await _repository.UpdateBarnAsync(barn);
        }
    }
}
