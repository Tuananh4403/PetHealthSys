using PetCareSystem.Services.Services.Models.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Records
{
    public interface IRecordServices
    {
        Task<bool> CreateRecordAsync(CreateRecordingReq createRecordReq, string token);
    }
}
