
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
