using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Services.Models.Recording;
using System.Threading.Tasks;


namespace PetCareSystem.Services.Services.Records
{
    public class RecordServices : IRecordServices
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IDoctorRepository _doctorRepository;

        public RecordServices(IRecordRepository recordRepository, IDoctorRepository doctorRepository)
        {
            _recordRepository = recordRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<bool> CreateRecordAsync(CreateRecordingReq createRecordReq, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            var doctor = _doctorRepository.GetDoctorByUserId(userId);
            if(doctor == null)
            {
                return false;
            }
            var record = new Record
            {
                DoctorId = createRecordReq.DoctorId,
                PetId = createRecordReq.PetId,
                BarnId = createRecordReq.BarnId,
                DetailPrediction = createRecordReq.DetailPrediction,
                Conclude = createRecordReq.Conclude
            };

            return await _recordRepository.AddAsync(record);
        }
    }
}
