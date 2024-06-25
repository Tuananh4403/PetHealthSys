using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Models.Recording;
using System.Threading.Tasks;


namespace PetCareSystem.Services.Services.Records
{
    public class RecordServices(IRecordRepository recordRepository, IDoctorRepository doctorRepository) : IRecordServices
    {
        private readonly IRecordRepository _recordRepository = recordRepository;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;

        public async Task<ApiResponse<string>> CreateRecordAsync(CreateRecordingReq createRecordReq, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            var doctor = _doctorRepository.GetDoctorByUserId(userId);
            if(doctor == null)
            {
                return new ApiResponse<string>("Doctor not exist", true);
            }
            var record = new Record
            {
                DoctorId = createRecordReq.DoctorId,
                PetId = createRecordReq.PetId,
                BarnId = createRecordReq.BarnId,
                DetailPrediction = createRecordReq.DetailPrediction,
                Conclude = createRecordReq.Conclude
            };
            bool result = await _recordRepository.AddAsync(record);
            if(result){
                return new ApiResponse<string>("Create Success!");
            }
            return new ApiResponse<string>("Create Record fail!", true);
        }
    }
}
