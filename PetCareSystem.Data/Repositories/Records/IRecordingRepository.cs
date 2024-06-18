using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Records
{
    public interface IRecordingRepository
    {

        Task<bool> IsDoctorId(int doctorId);
        Task<bool> CreateRecordingAsyn(Record record);

        Task<bool> CreateRecordingDetailAsyn(RecordDetail recordDetail);

        Task<bool> CreateBarn(Barn barn);
        Task<Boolean> SaveChangesAsync();
    }
}
