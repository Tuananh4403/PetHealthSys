using PetCareSystem.Data.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Barns
{
    public interface IBarnRepository
    {
        Task<bool> CreateBarnAsync(Barn barn);
        Task<bool> DeleteBarnAsync(int id);
        Task<IList<Barn>> GetBarns();

        Task<IList<Barn>> GetBarnsAsync(bool status);
        Task<bool> UpdateBarnAsync(Barn barn);
    }
}
