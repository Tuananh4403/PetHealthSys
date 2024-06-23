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

        public async Task<bool> IsDoctorId(int doctorId)
        {
            return await _dbContext.Doctors.AnyAsync(d => d.Id == doctorId);
        }
        public async Task<bool> CreateRecordingAsyn(Record record)
        {
            _dbContext.Records.Add(record);
            return await SaveChangesAsync();
        }

        public async Task<bool> CreateRecordingDetailAsyn(RecordDetail recordDetail)
        {
            _dbContext.RecordDetails.Add(recordDetail);
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

        public async Task<bool> CreateBarn(Barn barn)
        {
            _dbContext.Barns.Add(barn);
            return await SaveChangesAsync();
        }

        public async Task<Record> GetRecordByIdAsync(int recordId)
        {
            return await _dbContext.Records.FirstOrDefaultAsync(id => id.Id == recordId);
        }


        public async Task<bool> UpdateRecordAsync(int recordId, Record record)
        {
            try
            {
                var existingRecord = await _dbContext.Records.FirstOrDefaultAsync(r => r.Id == recordId);

                if (existingRecord == null)
                {
                    return false;
                }

                existingRecord.DoctorId = record.DoctorId;
                existingRecord.PetId = record.PetId;
                existingRecord.BarnId = record.BarnId;
                existingRecord.saveBarn = record.saveBarn;
                existingRecord.Conclude = record.Conclude;
                existingRecord.DetailPrediction = record.DetailPrediction;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<RecordDetail> GetRecordDetailByIdAsync(int recordDetailId)
        {
            return await _dbContext.RecordDetails.FirstOrDefaultAsync(id => id.Id == recordDetailId);
        }
    }
}