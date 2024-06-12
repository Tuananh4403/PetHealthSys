using PetCareSystem.Services.Models.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Pets
{
    public interface IPetService
    {
        Task<bool> RegisterPetAsync(PetRequest model, string token);
    }
}
