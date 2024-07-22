using Newtonsoft.Json;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.RecordDetails;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Data.Repositories.Services;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Models.Recording;
using PetCareSystem.Services.Services.Serivces;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;


namespace PetCareSystem.Services.Services.Records
{
    public class RecordServices(IRecordRepository recordRepository, IDoctorRepository doctorRepository, IRecordDetailRepository recordDetailRepository, IBookingRepository bookingRepository, IBarnRepository barnRepository, IServicesRepository servicesRepository) : IRecordServices
    {
        private readonly IRecordRepository _recordRepository = recordRepository;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly IRecordDetailRepository _recordDetailRepository = recordDetailRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IBarnRepository _barnRepository = barnRepository;
        private readonly IServicesRepository _servicesRepository = servicesRepository;

        public async Task<ApiResponse<string>> CreateRecordAsync(CreateRecordingReq createRecordReq, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            var doctor = await _doctorRepository.GetDoctorByUserId(userId);
            if (doctor == null)
            {
                return new ApiResponse<string>("Doctor not exist", true);
            }
            decimal total = 0;
            var record = new Record
            {
                DoctorId = doctor.Id,
                PetId = createRecordReq.PetId,
                PetHeight = createRecordReq.Height ?? 0,
                PetWeigth = createRecordReq.Weight,
                SaveBarn = createRecordReq.SaveBarn,
                Status = RecordStautus.Continue,
                DetailPrediction = createRecordReq.DetailPrediction,
                Conclude = createRecordReq.Conclude
            };
            bool result = await _recordRepository.AddAsync(record);
            if (result)
            {
                if (createRecordReq.SaveBarn)
                {
                    var barn = await _barnRepository.GetByIdAsync((int)createRecordReq.BarnId);
                    if (barn == null || (bool)barn.Status)
                    {
                        return new ApiResponse<string>("Barn does not exist", true);
                    }
                    else
                    {
                        record.BarnId = record.BarnId = barn.Id;
                        barn.Status = false;
                        await _barnRepository.UpdateAsync(barn);
                        if (!await _recordRepository.UpdateAsync(record))
                        {
                            return new ApiResponse<string>("Can not create Barn", true);
                        };
                    }
                }
                if (createRecordReq.ServiceQuantities != null)
                {
                    if (createRecordReq.ServiceQuantities.Count > 0)
                    {
                        foreach (var detail in createRecordReq.ServiceQuantities)
                        {
                            if (detail.Value == 0)
                            {
                                return new ApiResponse<string>("Quantity cannot be zero!", true);
                            }
                            var servcie = await _servicesRepository.GetByIdAsync((int)detail.Key);
                            var recordDetail = new RecordDetail
                            {
                                RecordId = record.Id,
                                ServiceId = detail.Key,
                                Quantity = detail.Value,
                            };
                            total += (decimal)servcie.Price * (decimal)detail.Value;
                            await _recordDetailRepository.AddAsync(recordDetail);
                        }
                    }
                    record.Total = total;
                    await _recordRepository.UpdateAsync(record);
                    return new ApiResponse<string>(message: "Create Success!", false);
                }
            }
            return new ApiResponse<string>("Create Record fail!", true);
        }

        public async Task<ApiResponse<string>> CreateRecordByBookingAsync(int bookingId, string token)
        {
            var booking = await _bookingRepository.GetBookingDetail(bookingId);
            var doctor = await _doctorRepository.GetDoctorByUserId(CommonHelpers.GetUserIdByToken(token));
            string message = "Create record Fails";
            bool result = false;
            if (booking.Status != BookingStatus.Confirmed)
            {
                message = "Booking not confirmed!";
            }
            else
            {
                if (doctor == null)
                {
                    message = "Doctor does not exist!";
                }
                else
                {
                    Record record = new Record
                    {
                        DoctorId = doctor.Id,
                        PetId = booking.PetId,
                        SaveBarn = false
                    };
                    result = await _recordRepository.AddAsync(record);
                    if (result)
                    {
                        var savedRecord = await _recordRepository.GetByIdAsync(record.Id);
                        savedRecord.RecordDetails = new List<RecordDetail>();
                        if (booking.BookingServices.Count > 0)
                        {
                            foreach (var service in booking.BookingServices)
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
            return new PaginatedApiResponse<Record>(records, totalCount, pageNumber, pageSize);
        }

        public async Task<ApiResponse<Object>> GetRecordHis(int petId)
        {
            try
            {
                var record = await _recordRepository.GetRecordDetail(petId);
                return new ApiResponse<Object>(record, "Get data success");
            }
            catch
            {
                return new ApiResponse<Object>(null, message: "Get data fails!");

            }
        }
        public async Task<ApiResponse<Record>> GetDetail(int id)
        {
            try
            {
                var record = await _recordRepository.GetById(id);
                return new ApiResponse<Record>(record, "Get data success");
            }
            catch
            {
                return new ApiResponse<Record>(null, message: "Get data fails!");

            }
        }
        public async Task<ApiResponse<string>> FinishRecord(int recordId)
        {
            Record record = await _recordRepository.GetByIdAsync(recordId);
            decimal total = 0;
            if (record.Status == RecordStautus.Completed)
            {
                return new ApiResponse<string>("Record was completed!", true);
            }
            foreach (var detail in record.RecordDetails)
            {
                total += (decimal)detail.Service.Price * (decimal)detail.Quantity;
            }
            record.Status = RecordStautus.Completed;
            if (await _recordRepository.UpdateAsync(record))
            {
                return new ApiResponse<string>("Record has finished", false);
            }
            return new ApiResponse<string>("Finish record fail!", true);
        }
    }
}
