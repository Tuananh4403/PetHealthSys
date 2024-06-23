using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Models.Barn;

namespace PetCareSystem.Services.Services.Barn
{
    public interface IBarnServices
    {
        Task<bool> CreateBarnAsync(BarnReq barnReq);

        Task<bool> UpdateBarnAsync(int id,BarnReq barnReq);

        Task<IList<Data.Entites.Barn>> GetBarn();

        Task<IList<Data.Entites.Barn>> GetBarnStatusFalse(bool status);

        Task<bool> DeleteBarnAsync(int id);
    }
}
