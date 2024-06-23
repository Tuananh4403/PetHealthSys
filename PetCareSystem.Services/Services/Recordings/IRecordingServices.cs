using PetCareSystem.Services.Models.Recording;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareSystem.Services.Services.Models.Recording;

namespace PetCareSystem.Services.Services.Recordings
{
    public interface IRecordingServices
    {
        Task<bool> CreateRecordAsync(RecordingReq createRecordReq, int recordId, int serviceId);

        Task<Record> GetRecordByIdAsync(int recordId);

        Task<RecordDetail> GetRecordDetailByIdAsync(int recordDetailId);
        Task<bool> UpdateRecordAsync(int recordId ,RecordingReq recordReq);
    }
}
