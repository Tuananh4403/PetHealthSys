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
using Microsoft.Extensions.Logging;
using System.Data.Entity.Infrastructure;


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
            var doctor = await _doctorRepository.GetDoctorByUserId(userId);
            if (doctor == null)
            {
                return new ApiResponse<string>("Doctor not exist", true);
            }

            var record = new Record
            {
                DoctorId = doctor.Id,
                PetId = createRecordReq.PetId,
                PetHeight = createRecordReq.Height,
                PetWeigth = createRecordReq.Weight,
                BarnId = createRecordReq.SaveBarn ? createRecordReq.BarnId : (int?)null,
                DetailPrediction = createRecordReq.DetailPrediction,
                Conclude = createRecordReq.Conclude
            };

            try
            {
                bool result = await _recordRepository.AddAsync(record);
                if (result)
                {
                    if (createRecordReq.ServiceQuantities != null && createRecordReq.ServiceQuantities.Count > 0)
                    {
                        foreach (var detail in createRecordReq.ServiceQuantities)
                        {
                            if (detail.Value == 0)
                            {
                                return new ApiResponse<string>("Quantity cannot be zero!");
                            }
                            var recordDetail = new RecordDetail
                            {
                                RecordId = record.Id,
                                ServiceId = detail.Key,
                                Quantity = detail.Value,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            };
                            await _recordDetailRepository.AddAsync(recordDetail);
                        }
                    }
                    return new ApiResponse<string>("Create Success!");
                }
                return new ApiResponse<string>("Create Record fail!", true);
            }
            catch (DbUpdateException dbEx)
            {
                return new ApiResponse<string>($"Create Record fail: {dbEx.Message}", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>($"Create Record fail: {ex.Message}", true);
            }
        }



        public async Task<ApiResponse<object>> GetMedicalHistory(int petId, string token)
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

            var isRecord = await _recordRepository.GetByIdAsync(petId);
            if (isRecord == null)
            {
                throw new ArgumentException("Record not found");
            }
            

            var infor = await _recordRepository.GetInfor(petId);
            var medicalHistory = await _recordRepository.GetMedicalHistory(petId);
            var response = new
            {
                infor,
                medicalHistory,
            };
            return new ApiResponse<object>(response);
        }
    }
}
