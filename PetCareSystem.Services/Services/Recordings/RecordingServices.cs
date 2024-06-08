using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Records;
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

        public async Task<bool> CreateRecordAsync(CreateRecordingReq createRecordReq)
        {
            // Kiểm tra xem ID có phải của một bác sĩ không
            if (!await IsDoctorId(createRecordReq.DoctorId))
            {
                return false; // Không phải bác sĩ, trả về false
            }

            // Tạo bản ghi mới
            var record = new Record
            {
                DoctorId = createRecordReq.DoctorId,
                PetId = createRecordReq.PetId,
                BarnId = createRecordReq.BarnId,
                DetailPrediction = createRecordReq.DetailPrediction,
                Conclude = createRecordReq.Conclude
            };

            return await _recordRepository.CreateRecordingAsyn(record);
        }

        public async Task<bool> IsDoctorId(int doctorId)
        {
            // Kiểm tra trong cơ sở dữ liệu xem ID có phải của một bác sĩ không
            return await _recordRepository.IsDoctorId(doctorId);
        }
    }
}
