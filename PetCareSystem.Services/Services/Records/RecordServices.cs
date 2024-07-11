using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.RecordDetails;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Models.Recording;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;


namespace PetCareSystem.Services.Services.Records
{
    public class RecordServices(IRecordRepository recordRepository, IDoctorRepository doctorRepository, IRecordDetailRepository recordDetailRepository, IBookingRepository bookingRepository) : IRecordServices
    {
        private readonly IRecordRepository _recordRepository = recordRepository;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly IRecordDetailRepository _recordDetailRepository = recordDetailRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;

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
            if(result && createRecordReq.ServiceQuantities != null){
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
                        await _recordDetailRepository.AddAsync(recordDetail);
                    }
                }
                return new ApiResponse<string>("Create Success!");
            }
            return new ApiResponse<string>("Create Record fail!", true);
        }

        public async Task<ApiResponse<string>> CreateRecordByBookingAsync(int bookingId, string token)
        {
            var booking = await _bookingRepository.GetBookingDetail(bookingId);
            var doctor = await _doctorRepository.GetDoctorByUserId(CommonHelpers.GetUserIdByToken(token));
            string message = "Create record Fails";
            bool result = false;
            if(booking.Status != BookingStatus.Confirmed){
                message = "Booking not confirmed!";
            }else{
                if(doctor == null){
                    message = "Doctor does not exist!";
                }else{
                    Record record = new Record{
                    DoctorId = doctor.Id,
                    PetId = booking.PetId,
                    SaveBarn = false
                    };
                    result = await _recordRepository.AddAsync(record);
                    if(result){
                        var savedRecord = await _recordRepository.GetByIdAsync(record.Id);
                        savedRecord.RecordDetails = new List<RecordDetail>();
                        if(booking.BookingServices.Count > 0)
                        {
                            foreach(var service in booking.BookingServices)
                            {
                                // var serviceDetail = service.Service;
                                var recordDetail = new RecordDetail
                                {
                                    RecordId = record.Id,
                                    ServiceId = service.ServiceId,
                                    Quantity = service.Quantity,
                                };
                                savedRecord.RecordDetails.Add(recordDetail);
                                await _recordDetailRepository.AddAsync(recordDetail);
                            }
                            result = await _recordRepository.UpdateAsync(savedRecord);
                        }
                        message = "Create record success!";
                    }
                }
            }
            return new ApiResponse<string>(message, result);
        }

        public async Task<PaginatedApiResponse<Record>> GetListRecord(string? petName, string? nameOfCustomer, int pageNumber = 1, int pageSize = 10)
        {
             var (records, totalCount) = await _recordRepository.GetListRecord(petName, nameOfCustomer, pageNumber, pageSize);
            if (!records.Any())
            {
                return new PaginatedApiResponse<Record>("No Record found", true);
            }
            return new PaginatedApiResponse<Record>(records, totalCount,pageNumber, pageSize);
        }

        public async Task<ApiResponse<Record?>> GetRecordHis(int petId)
        {
            try
            {
                var record = await _recordRepository.GetRecordDetail(petId);
                return new ApiResponse<Record?>(record, "Get data success");
            }catch
            {
                return new ApiResponse<Record?>(null, message: "Get data fails!");

            }
        }
    }
}
