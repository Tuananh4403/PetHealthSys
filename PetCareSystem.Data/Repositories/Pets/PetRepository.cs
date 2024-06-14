
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Pets
{
    public class PetRepository : IPetRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public PetRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddPetAsync(Pet pet)
        {
            await _dbContext.Pets.AddAsync(pet);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> PetExists(int petId)
        {
            return await _dbContext.Pets.AnyAsync(p => p.Id == petId);
        }

        public async Task<Pet> GetPetByIdAsync(int petId)
        {
            return await _dbContext.Pets.FindAsync(petId);
        }

        public async Task<IList<Pet>> GetListPet(string petName, string nameOfCustomer, string kindOfPet, string speciesOfPet, bool? genderOfPet, DateTime? birthdayOfPet)
        {
            var query = _dbContext.Pets.AsQueryable();

            if (!string.IsNullOrEmpty(petName))
            {
                query = query.Where(p => p.PetName.Contains(petName));
            }
            if (!string.IsNullOrEmpty(nameOfCustomer))
            {
                query = query.Include(p => p.Customer).Where(p => p.Customer.User.Username.Contains(nameOfCustomer));
            }
            if (!string.IsNullOrEmpty(kindOfPet))
            {
                query = query.Where(p => p.KindOfPet.Contains(kindOfPet));
            }
            if (!string.IsNullOrEmpty(speciesOfPet))
            {
                query = query.Where(p => p.Species.Contains(speciesOfPet));
            }
            if (genderOfPet.HasValue)
            {
                query = query.Where(p => p.Gender == genderOfPet.Value);
            }
            if (birthdayOfPet.HasValue)
            {
                query = query.Where(p => p.Birthday.Date == birthdayOfPet.Value.Date);
            }

            return query.ToList<Pet>();
        }

        public async Task<bool> UpdatePet(int id, string petName, string kindOfPet, bool gender, DateTime birthday, string species)
        {
            var pet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null)
            {
                return false; 
            }

            
            pet.PetName = petName;
            pet.KindOfPet = kindOfPet;
            pet.Gender = gender; 
            pet.Birthday = birthday;
            pet.Species = species;

            _dbContext.Pets.Update(pet);
            await _dbContext.SaveChangesAsync();

            return true; 
        }

        public async Task<bool> DeletePet(int id)
        {
            var pet = await _dbContext.Pets.FindAsync(id);
            if (pet == null)
            {
                return false; 
            }

            bool isBooked = await _dbContext.Bookings.AnyAsync(b => b.PetId == id);
            bool hasRecords = await _dbContext.Records.AnyAsync(r => r.PetId == id);
            if (isBooked || hasRecords)
            {
                return false; 
            }

            _dbContext.Pets.Remove(pet);
            await _dbContext.SaveChangesAsync();

            return true; 
        }

    }
}
