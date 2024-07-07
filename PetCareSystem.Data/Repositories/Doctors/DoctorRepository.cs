using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Doctors
{
    public class DoctorRepository(PetHealthDBContext dbContext, ILogger<DoctorRepository> logger) : BaseRepository<Doctor>(dbContext, logger), IDoctorRepository
    {
        public async Task<Doctor> GetDoctorByUserId(int? id)
        {
            return await dbContext.Doctors.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<(IEnumerable<Doctor> doctors, int totalCount)> GetListDoctor(string? searchString, int pageNumber = 1, int pageSize = 10)
        {
            var query = dbContext.Doctors.AsQueryable()
                                         .Include(d => d.User);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Doctor, User>)query.Where(d => d.User.FirstName.Contains(searchString) || d.User.LastName.Contains(searchString));
            }

            var totalCount = await query.CountAsync();
            var doctors = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (doctors, totalCount);
        }
    }
}
