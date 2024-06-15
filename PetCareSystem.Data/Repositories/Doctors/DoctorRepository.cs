using Microsoft.EntityFrameworkCore;
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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public DoctorRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddDoctorAsync(Doctor doc)
        {
            await _dbContext.Doctors.AddAsync(doc);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            return await _dbContext.Doctors.SingleOrDefaultAsync(u => u.UserId == id);
        }
    }
}
