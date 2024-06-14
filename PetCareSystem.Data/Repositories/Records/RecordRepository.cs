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
    public class RecordRepository : IRecordRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public RecordRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddRecordAsync(Record record)
        {
            _dbContext.Records.Add(record);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0; 
        }

        public async Task<bool> AddRecordDetailsAsync(RecordDetail recordDetail)
        {
            _dbContext.RecordDetails.Add(recordDetail);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0; 
        }
    }

}
