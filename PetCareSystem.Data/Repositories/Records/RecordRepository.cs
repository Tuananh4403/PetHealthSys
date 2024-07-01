using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Records
{
    public class RecordRepository(PetHealthDBContext dbContext, ILogger<RecordRepository> logger) : BaseRepository<Record>(dbContext, logger), IRecordRepository
    {
        public async Task<Record?> GetRecordDetail(int petId)
        {
            var query = dbContext.Records.AsQueryable();
            query = query.Include(x => x.Pet)
                         .Include(x => x.Doctor)
                         .Include(x => x.RecordDetails)
                            .ThenInclude(rd =>rd.Service)
                         .Include(x =>x.Barn)
                         .Where(x => x.PetId == petId);
            var recordsDetail = await query.SingleOrDefaultAsync();
            return recordsDetail;
        }
    }
}
