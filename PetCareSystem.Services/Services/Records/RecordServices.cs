using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.RecordDetails;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Data.Repositories.Pets;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Models.Recording;
using PetCareSystem.Services.Services.Bookings;
using System.Threading.Tasks;


namespace PetCareSystem.Services.Services.Records
{
    public class RecordServices(IRecordRepository recordRepository, IDoctorRepository doctorRepository, IRecordDetailRepository recordDetailRepository, IPetRepository petRepository,IBookingServices bookingServices,IUserRepository userRepository): IRecordServices
    {
        private readonly IRecordRepository _recordRepository = recordRepository;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly IRecordDetailRepository _recordDetailRepository = recordDetailRepository;
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IBookingServices _bookingServices;
        private readonly IUserRepository _userRepository;

        public async Task<ApiResponse<string>> CreateRecordAsync(CreateRecordingReq createRecordReq, string token) 
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            var doctor =await _doctorRepository.GetDoctorByUserId(userId);
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

        public async Task<ApiResponse<object>> GetRecordById(int recordId, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }

            var isDoctor = await _doctorRepository.GetDoctorByUserId(userId.Value);
            if (isDoctor == null)
            {
                throw new ArgumentException("Doctor does not exist");
            }

            var isRecord = await _recordRepository.GetByIdAsync(recordId);
            if (isRecord == null)
            {
                throw new ArgumentException("Record not found");
            }
            

            var response = await _recordRepository.GetRecord(recordId);
            return new ApiResponse<object>(response);
        }
    }
}
