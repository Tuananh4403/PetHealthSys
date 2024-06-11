using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Pets
{
    public interface IPetRepository
    {
        Task AddPetAsync(Pet pet);
    }
}
