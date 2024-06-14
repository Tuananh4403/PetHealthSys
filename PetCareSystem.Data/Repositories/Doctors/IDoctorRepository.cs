using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Doctors
{
    public interface IDoctorRepository
    {
        int? GetDoctorIdFromToken(string token);
        bool IsDoctor(int doctorId);
    }
}
