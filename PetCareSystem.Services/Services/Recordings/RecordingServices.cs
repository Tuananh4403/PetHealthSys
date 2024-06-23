using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Models.Recording;
using PetCareSystem.Services.Services.Models.Recording;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Recordings
{
    public class RecordingServices : IRecordingServices
    {
        private readonly IRecordingRepository _recordRepository;
        private readonly IBarnRepository _banRepository;

        public RecordingServices(IRecordingRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<bool> CreateRecordAsync(RecordingReq recordReq, int recordId, int serviceId)
        {
            if (!await _recordRepository.IsDoctorId(recordReq.DoctorId))
            {
                return false; 
            }

            var record = new Record
            {
                DoctorId = recordReq.DoctorId,
                PetId = recordReq.PetId,
                BarnId = recordReq.BarnId,
                saveBarn = recordReq.SaveBarn,
                Conclude = recordReq.Conclude,
                DetailPrediction = recordReq.DetailPrediction,
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

        public async Task<Record> GetRecordByIdAsync(int recordId)
        {
            return await _recordRepository.GetRecordByIdAsync(recordId);
        }

        public async Task<RecordDetail> GetRecordDetailByIdAsync(int recordDetailId)
        {
            return await _recordRepository.GetRecordDetailByIdAsync(recordDetailId);
        }

        public async Task<bool> UpdateRecordAsync(int recordId, RecordingReq recordReq)
        {
            var isRecord = await _recordRepository.GetRecordByIdAsync(recordId);
            if (isRecord == null)
            {
                return false; 
            }

            isRecord.DoctorId = recordReq.DoctorId;
            isRecord.PetId = recordReq.PetId;
            isRecord.BarnId = recordReq.BarnId;
            isRecord.saveBarn = recordReq.SaveBarn;
            isRecord.Conclude = recordReq.Conclude;
            isRecord.DetailPrediction = recordReq.DetailPrediction;

            if (!await _recordRepository.UpdateRecordAsync( recordId,isRecord))
            {
                return false; 
            }


            return true; 
        }

    }
}
