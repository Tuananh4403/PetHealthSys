using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Doctors
{
    public class DoctorRepository

    {
        private readonly PetHealthDBContext _dbContext;

        public DoctorRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int? GetDoctorIdFromToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;


                var userIdClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "id");


                if (int.TryParse(userIdClaim?.Value, out int userId))
                {
                    return userId;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public bool IsDoctor(int doctorId)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(d => d.Id == doctorId);
            return doctor != null;
        }
    }
}
