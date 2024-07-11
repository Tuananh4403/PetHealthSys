using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Records
{
    public class RecordRepository(PetHealthDBContext dbContext, ILogger<RecordRepository> logger) : BaseRepository<Record>(dbContext, logger), IRecordRepository
    {
        public async Task<(IEnumerable<Record> Records, int totalCount)> GetListRecord(string? petName, string? nameOfCustomer, int pageNumber, int pageSize)
        {
             var query = dbContext.Records.AsQueryable();
            query = query.Include(x => x.Pet)
                            .ThenInclude(p => p.Customer)
                            .ThenInclude(c => c.User)
                         .Include(x => x.Doctor)
                         .Include(x =>x.Barn);
            if(!petName.IsNullOrEmpty()){
                query = query.Where(rc => rc.Pet.PetName.Contains(petName));
            }
            if(!nameOfCustomer.IsNullOrEmpty()){
                query = query.Where(rc => rc.Pet.Customer.User.FirstName.Contains(nameOfCustomer) || rc.Pet.Customer.User.LastName.Contains(nameOfCustomer));
            }
            var result = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var count = await query.CountAsync();
            return (result, count);
        }

        public async Task<Record?> GetRecordDetail(int petId)
        {
            var query = dbContext.Records.AsQueryable();
            query = query.Include(x => x.Pet)
                         .Include(x => x.Doctor)
                         .Include(x => x.RecordDetails)
                            .ThenInclude(rd =>rd.Service)
                         .Include(x =>x.Barn)
                         .Where(x => x.PetId == petId);
            var recordsDetail = await query.SingleOrDefaultAsync();
            return recordsDetail;
        }
    }
}
