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
    }
}
