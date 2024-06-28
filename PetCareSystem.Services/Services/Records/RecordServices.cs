using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.RecordDetails;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Models.Recording;
using System.Threading.Tasks;


namespace PetCareSystem.Services.Services.Records
{
    public class RecordServices(IRecordRepository recordRepository, IDoctorRepository doctorRepository, IRecordDetailRepository recordDetailRepository) : IRecordServices
    {
        private readonly IRecordRepository _recordRepository = recordRepository;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly IRecordDetailRepository _recordDetailRepository = recordDetailRepository;

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
                DoctorId = doctor.Id,
                PetId = createRecordReq.PetId,
                PetHeight = createRecordReq.Height,
                PetWeigth = createRecordReq.Weight,
                BarnId = createRecordReq.BarnId,
                DetailPrediction = createRecordReq.DetailPrediction,
                Conclude = createRecordReq.Conclude
            };
            bool result = await _recordRepository.AddAsync(record);
            if(result){
                if(createRecordReq.ServiceQuantities.Count > 0)
                {
                    foreach(var detail in createRecordReq.ServiceQuantities)
                    {
                        if(detail.Value == 0)
                        {
                            return new ApiResponse<string>("Quantity cannot be zero!");
                        }
                        var recordDetail = new RecordDetail
                        {
                            RecordId = record.Id,
                            ServiceId = detail.Key,
                            Quantity = detail.Value,
                        };
                        _recordDetailRepository.AddAsync(recordDetail);
                    }
                }
                return new ApiResponse<string>("Create Success!");
            }
            return new ApiResponse<string>("Create Record fail!", true);
        }
    }
}
