using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Data.Repositories.Users;

namespace PetCareSystem.Data.Repositories.Records
{

    public class RecordRepository(PetHealthDBContext dbContext, ILogger<RecordRepository> logger) : BaseRepository<Record>(dbContext, logger), IRecordRepository
    {
        public async Task<object> GetInfor(int petId)
        {
            try
            {
                var recordForm = await dbContext.Records
                    .Where(r => r.Id == petId)
                    .Select(r => new
                    {
                        r.Pet.PetName,
                        r.Pet.KindOfPet,
                        r.Pet.Gender,
                        r.Pet.Birthday,
                        r.Pet.Species,

                        // Customer ìno
                        r.Pet.Customer.User.FirstName,
                        r.Pet.Customer.User.LastName,
                        r.Pet.Customer.User.Email,
                        r.Pet.Customer.User.PhoneNumber,
                        r.Pet.Customer.User.Address,

                    })
                    .FirstOrDefaultAsync();

                if (recordForm == null)
                {
                    throw new KeyNotFoundException($"Record with PetId {petId} not found.");
                }

                return recordForm;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching record form for recordId {petId}: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<IList<dynamic>> GetMedicalHistory(int petId)
        {
            try
            {
                var medicalHistory = await dbContext.Records
                    .Where(r => r.PetId == petId)
                    .OrderByDescending(r => r.CreatedAt)
                    .Select(r => new
                    {
                        r.CreatedAt,
                        r.saveBarn,
                        r.BarnId,
                        r.DetailPrediction,
                        r.Conclude
                    })
                    .ToListAsync();

                if (medicalHistory == null || !medicalHistory.Any())
                {
                    throw new KeyNotFoundException($"No medical records found for PetId {petId}.");
                }

                return medicalHistory.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching medical history for PetId {petId}: {ex.Message}", ex);
                throw;
            }
        }

    }

}
