using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Pets
{
    public interface IPetRepository
    {
        Task AddPetAsync(Pet pet);
        Task<bool> PetExists(int petId);
        Task<Pet> GetPetByIdAsync(int petId);
        Task<IList<Pet>> GetListPet(string petName, string nameOfCustomer, string kindOfPet, string speciesOfPet, bool? genderOfPet, DateTime? birthdayOfPet);
        Task<bool> UpdatePet(int id, string petName, string kindOfPet, bool gender, DateTime birthday, string species);
        Task<bool> DeletePet(int id);
    }
}
