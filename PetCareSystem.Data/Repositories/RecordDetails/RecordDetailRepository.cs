using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.RecordDetails
{
    public class RecordDetailRepository(PetHealthDBContext dbContext, ILogger<RecordDetailRepository> logger) : BaseRepository<RecordDetail>(dbContext, logger), IRecordDetailRepository
    {
        public async Task<int[]>GetServiceList(int recordDetailId)
        {
            try
            {
                return await dbContext.RecordDetails
                    .Where(rd => rd.RecordId == recordDetailId)
                    .Select(rd => rd.ServiceId)
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching service list for recordId {recordDetailId}: {ex.Message}", ex);
                throw;
            }
        }
    }
}
