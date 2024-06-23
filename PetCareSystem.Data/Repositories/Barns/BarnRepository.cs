using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Barns
{
    public class BarnRepository : IBarnRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public BarnRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateBarnAsync(Barn barn)
        {
            _dbContext.Barns.Add(barn);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteBarnAsync(int id)
        {
            var barn = await _dbContext.Barns.FindAsync(id);
            if (barn == null) return false;
            _dbContext.Barns.Remove(barn);
            return await SaveChangesAsync();
        }

        public async Task<IList<Barn>> GetBarns()
        {
            return await _dbContext.Barns.ToListAsync();
        }

        public async Task<IList<Barn>> GetBarnsAsync(bool status)
        {
            return await _dbContext.Barns.Where(b => b.Status == status).ToListAsync();
        }

        public async Task<bool> UpdateBarnAsync(Barn barn)
        {
            var existingBarn = await _dbContext.Barns.FindAsync(barn.Id);
            if (existingBarn == null) return false;

            existingBarn.DateStart = barn.DateStart;
            existingBarn.DateEnd = barn.DateEnd;
            existingBarn.Status = barn.Status;
            existingBarn.Result = barn.Result;

            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Log or handle the exception as needed
                return false;
            }
        }
    }
}
