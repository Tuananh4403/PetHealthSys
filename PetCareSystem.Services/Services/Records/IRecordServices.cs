using PetCareSystem.Services.Services.Models.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Recordings
{
    public interface IRecordServices
    {
        Task<bool> CreateRecordAsync(RecordRequest model, string token);
    }
}
