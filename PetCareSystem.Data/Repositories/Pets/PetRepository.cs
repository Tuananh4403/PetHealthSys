
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class PetRepository(PetHealthDBContext dbContext, ILogger<PetRepository> logger) : BaseRepository<Pet>(dbContext, logger), IPetRepository
    {
        public async Task<bool> PetExists(int petId)
        {
            return await dbContext.Pets.AnyAsync(p => p.Id == petId);
        }

        public async Task<IList<Pet>> GetListPet(string petName, string nameOfCustomer, string kindOfPet, string speciesOfPet, bool? genderOfPet, DateTime? birthdayOfPet)
        {
            var query = dbContext.Pets.AsQueryable();

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

        public async Task<Pet?> GetMedicalHis(int petId)
        {
            var query = dbContext.Pets.AsQueryable();
            query = query.Include(p => p.Records)
                         .Include(p => p.Customer)
                         .Where(x => x.Id == petId);

            var pet = await query.Select(p => new Pet
            {
                Id = p.Id,
                PetName = p.PetName,
                KindOfPet = p.KindOfPet,
                Gender = p.Gender,
                Birthday = p.Birthday,
                Species = p.Species,
                CustomerId = p.CustomerId,
                Customer = new Customer
                {
                    Id = p.Customer.Id,
                    User = p.Customer.User != null ? new User
                    {
                        Id = p.Customer.User.Id,
                        FirstName = p.Customer.User.FirstName,
                        LastName = p.Customer.User.LastName
                    } : null
                },
                Records = p.Records.Select(rc => new Record
                {
                    Id = rc.Id,
                    DoctorId = rc.DoctorId,
                    Doctor = new Doctor
                    {
                        Id = rc.Doctor.Id,
                        User =  new User
                        {
                            Id = rc.Doctor.User.Id,
                            FirstName = rc.Doctor.User.FirstName,
                            LastName = rc.Doctor.User.LastName,
                        } 
                    },
                    saveBarn = rc.saveBarn,
                    BarnId = rc.BarnId,
                    DetailPrediction = rc.DetailPrediction,
                    Conclude = rc.Conclude,
                    CreatedAt = rc.CreatedAt,


                }).ToList(),
            }).SingleOrDefaultAsync();
            return pet;
        }

        //public async Task<bool> UpdatePet(int id, string petName, string kindOfPet, bool gender, DateTime birthday, string species)
        //{
        //    var pet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
        //    if (pet == null)
        //    {
        //        return false; 
        //    }


        //    pet.PetName = petName;
        //    pet.KindOfPet = kindOfPet;
        //    pet.Gender = gender; 
        //    pet.Birthday = birthday;
        //    pet.Species = species;

        //    _dbContext.Pets.Update(pet);
        //    await _dbContext.SaveChangesAsync();

        //    return true; 
        //}

        //public async Task<bool> DeletePet(int id)
        //{
        //    var pet = await _dbContext.Pets.FindAsync(id);
        //    if (pet == null)
        //    {
        //        return false; 
        //    }

        //    bool isBooked = await _dbContext.Bookings.AnyAsync(b => b.PetId == id);
        //    bool hasRecords = await _dbContext.Records.AnyAsync(r => r.PetId == id);
        //    if (isBooked || hasRecords)
        //    {
        //        return false; 
        //    }

        //    _dbContext.Pets.Remove(pet);
        //    await _dbContext.SaveChangesAsync();

        //    return true; 
        //}

    }
}
