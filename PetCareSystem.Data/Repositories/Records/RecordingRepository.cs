using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Records
{
    public class RecordingRepository : IRecordingRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public RecordingRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateRecordingAsyn(Record record)
        {
             _dbContext.Records.Add(record);
            return await SaveChangesAsync();
        }

        public async Task<bool> IsDoctorId(int doctorId)
        {
            return await _dbContext.Doctors.AnyAsync(d => d.Id == doctorId);
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
