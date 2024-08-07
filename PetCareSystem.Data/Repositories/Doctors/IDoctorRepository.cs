﻿
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Doctors
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetDoctorByUserId(int? id);
        Task<(IEnumerable<Doctor> doctors, int totalCount)> GetListDoctor(string? searchString, int pageNumber = 1, int pageSize = 10);
    }
}
