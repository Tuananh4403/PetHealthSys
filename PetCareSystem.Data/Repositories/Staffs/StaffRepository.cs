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
    }
}
