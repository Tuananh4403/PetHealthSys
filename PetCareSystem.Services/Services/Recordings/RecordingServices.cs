using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Models.Recording;
using PetCareSystem.Services.Services.Models.Recording;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Recordings
{
    public class RecordingServices : IRecordingServices
    {
        private readonly IRecordingRepository _recordRepository;

        public RecordingServices(IRecordingRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<bool> CreateRecordAsync(CreateRecordingReq createRecordReq, int recordId, int serviceId, CreateRecordingDetailReq cre)
        {
            if (!await _recordRepository.IsDoctorId(createRecordReq.DoctorId))
            {
                return false; 
            }

            var record = new Record
            {
                DoctorId = createRecordReq.DoctorId,
                PetId = createRecordReq.PetId,
                BarnId = createRecordReq.BarnId
            };

            if(! await _recordRepository.CreateRecordingAsyn(record))
            {
                return false;
            }

            var recordDetail = new RecordDetail
            {
                RecordId = recordId,
                ServiceId = serviceId
            }; if(! await _recordRepository.CreateRecordingDetailAsyn(recordDetail))
            {
                return false;
            }

            return true;
        }
    }
}
