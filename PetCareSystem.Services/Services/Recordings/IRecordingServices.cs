using PetCareSystem.Services.Models.Recording;
using PetCareSystem.Services.Services.Models.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Recordings
{
    public interface IRecordingServices
    {
        Task<bool> CreateRecordAsync(CreateRecordingReq createRecordReq, int recordId, int serviceId, bool resultRecord, DateTime dateEnd);
    }
}
