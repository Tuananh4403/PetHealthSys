using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Staffs
{
    public class StaffRepository(PetHealthDBContext dbContext, ILogger<StaffRepository> logger) : BaseRepository<Staff>(dbContext, logger), IStaffRepository
    {
        public async Task<(IEnumerable<Staff> staffs, int totalCount)> GetListStaff(string? searchString, int pageNumber = 1, int pageSize = 10)
        {
             var query = dbContext.Staffs.AsQueryable()
                                         .Include(d => d.User);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Staff, User>)query.Where(d => d.User.FirstName.Contains(searchString) || d.User.LastName.Contains(searchString));
            }

            var totalCount = await query.CountAsync();
            var staffs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (staffs, totalCount);
        }

        public async Task<Staff> GetStaffByUserId(int userId)
        {
            return await dbContext.Staffs.FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}
