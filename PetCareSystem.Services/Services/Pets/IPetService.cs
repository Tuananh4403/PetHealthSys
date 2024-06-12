using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Booking;
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
        Task<PetRequest> GetPetDetailsAsync(int petId);
        Task<IList<Pet>> GetListPet(string petName, string nameOfCustomer, string kindOfPet, string speciesOfPet, bool? genderOfPet, DateTime? birthdayOfPet);
        Task<bool> UpdatePetAsync(int id, PetRequest updatePet);
        Task<bool> DeletePetAsync(int id);
    }
}
