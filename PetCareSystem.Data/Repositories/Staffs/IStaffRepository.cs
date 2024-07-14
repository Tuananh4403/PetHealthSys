using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Staffs
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<(IEnumerable<Staff> staffs, int totalCount)> GetListStaff(string? searchString, int pageNumber = 1, int pageSize = 10);

        Task<Staff> GetStaffByUserId(int userId);
    }
}
