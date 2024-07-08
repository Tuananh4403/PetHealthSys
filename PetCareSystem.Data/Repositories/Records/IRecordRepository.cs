using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Records
{
    public interface IRecordRepository : IRepository<Record>
    {
        Task<object> GetRecord(int recordId);
    }
}
