using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> IsDoctorId(int doctorId)
        {
            return await dbContext.Doctors.AnyAsync(d => d.Id == doctorId);
        }
    }
}
