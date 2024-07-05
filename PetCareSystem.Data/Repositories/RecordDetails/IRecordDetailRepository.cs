using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.RecordDetails
{
    public interface IRecordDetailRepository : IRepository<RecordDetail>
    {
        Task<int[]>GetServiceList(int recordDetailId);
    }
}
