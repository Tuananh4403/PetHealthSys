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

        Task<Record> GetRecordByIdAsync(int recordId);

        Task<RecordDetail> GetRecordDetailByIdAsync(int recordDetailId);

        Task<bool> UpdateRecordAsync(int recordId,Record record);
        Task<Boolean> SaveChangesAsync();
    }
}
