
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Doctors
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor cus);
        Task<Doctor> GetDoctorById(int id);
    }
}
