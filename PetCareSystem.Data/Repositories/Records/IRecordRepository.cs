using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Records
{
    public interface IRecordRepository : IRepository<Record>
    {
        Task<(IEnumerable<Record> Records, int totalCount)> GetListRecord(string? petName, string? nameOfCustomer, int pageNumber, int pageSize);
        Task<Record?> GetRecordDetail(int petId);
        Task<Record> GetById(int id);
    }
}
