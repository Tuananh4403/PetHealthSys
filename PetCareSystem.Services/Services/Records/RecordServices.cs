using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Repositories.Records;
using PetCareSystem.Services.Services.Models.Record;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Recordings
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

        public async Task<bool> CreateRecordAsync(RecordRequest model, string token)
        {
            var doctorId = _doctorRepository.GetDoctorIdFromToken(token);
            if (doctorId == null || !_doctorRepository.IsDoctor(doctorId.Value))
            {
                return false;
            }

            var record = new Record
            {
                DoctorId = doctorId.Value,
                PetId = model.PetId,
                DetailPrediction = model.DetailPrediction,
                Conclude = model.Conclude,
                BarnId = model.BarnId,
            };

            var recordId = await _recordRepository.AddRecordAsync(record);
            if (recordId)
            {
                var recordDetails = new RecordDetail
                {
                    RecordId = record.Id,
                    ServiceId = model.ServiceId,
                    Quantity = model.Quantity
                };
                return await _recordRepository.AddRecordDetailsAsync(recordDetails);
            }
            return false;
        }
    }

}

