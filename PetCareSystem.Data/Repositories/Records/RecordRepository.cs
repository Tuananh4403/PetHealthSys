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

    public class RecordRepository(PetHealthDBContext dbContext, ILogger<RecordRepository> logger) : BaseRepository<Barn>(dbContext, logger), IRecordRepository
    {
        public Task<bool> AddAsync(Record entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Record entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Record>> FindAsync(Expression<Func<Record, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetRecord(int recordId)
        {
            try
            {
                var recordForm = await dbContext.Records
                    .Where(r => r.Id == recordId)
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

                        //Medical
                        r.CreatedAt,
                        DoctorName = r.Doctor.User.FirstName +" "+r.Doctor.User.LastName,
                        r.saveBarn,
                        r.Barn.Result,
                        r.DetailPrediction,
                        r.Conclude

                    })
                    .FirstOrDefaultAsync();

                if (recordForm == null)
                {
                    throw new KeyNotFoundException($"Record with Id {recordId} not found.");
                }

                return recordForm;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching record form for recordId {recordId}: {ex.Message}", ex);
                throw;
            }
        }

        public Task<bool> UpdateAsync(Record entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Record>> IRepository<Record>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Record> IRepository<Record>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
