using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
                         .Include(x =>x.Barn);
            if(!petName.IsNullOrEmpty()){
                query = query.Where(rc => rc.Pet.PetName.Contains(petName));
            }
            if(!nameOfCustomer.IsNullOrEmpty()){
                query = query.Where(rc => rc.Pet.Customer.User.FirstName.Contains(nameOfCustomer) || rc.Pet.Customer.User.LastName.Contains(nameOfCustomer));
            }
            var result = await query.Select(r => new Record
            {
            Id = r.Id,
            SaveBarn = r.SaveBarn,
            DoctorId = r.DoctorId,
            Doctor = r.Doctor,
            PetWeigth = r.PetWeigth,
            PetHeight = r.PetHeight,
            Status = r.Status,
            DetailPrediction = r.DetailPrediction,
            Conclude = r.Conclude,
            PetId = r.PetId,
            Pet = r.Pet,
            BarnId = r.BarnId,
            Total = r.Total,

        })
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
        public async Task<Record> GetById(int id){
             var query = dbContext.Records.AsQueryable()
                                          .Include(x => x.Pet)
                                          .Include(x => x.RecordDetails)
                                            .ThenInclude(rc => rc.Service)
                                        .Where(x => x.Id == id);

                                        query = query.Select(r => new Record
            {
            Id = r.Id,
            SaveBarn = r.SaveBarn,
            DoctorId = r.DoctorId,
            Doctor = r.Doctor,
            PetWeigth = r.PetWeigth,
            PetHeight = r.PetHeight,
            Status = r.Status,
            DetailPrediction = r.DetailPrediction,
            Conclude = r.Conclude,
            PetId = r.PetId,
            Pet = r.Pet,
            BarnId = r.BarnId,
            Total = r.Total,
            RecordDetails = r.RecordDetails != null ? r.RecordDetails.Select(rd => new RecordDetail
                {
                    Id = rd.Id,
                    Service = new Service
                    {
                            Name = rd.Service.Name,
                            TypeId = rd.Service.TypeId,
                            Unit = rd.Service.Unit,
                            Price = rd.Service.Price,
                    },
                    Quantity = rd.Quantity
                }).ToList() : null

        });
            var result = await query.FirstOrDefaultAsync();
            return result;
        }
    }
}
