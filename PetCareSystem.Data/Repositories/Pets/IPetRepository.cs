using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Pets
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<bool> PetExists(int petId);
        Task<(IEnumerable<Pet> pets, int totalCount)> GetListPet(string? petName, string? nameOfCustomer, bool? saveBarn, int pageNumber = 1, int pageSize = 10);
        Task<Pet?> GetMedicalHis(int petId);
        Task<(IEnumerable<Pet> pets, int totalCount)> GetListPetByUserId(int? cusId, bool? saveBarn, int pageNumber = 1, int pageSize = 10);
    }
}
