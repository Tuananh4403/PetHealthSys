using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Staffs
{
    public class StaffRepository(DbContext context, ILogger<BaseRepository<Staff>> logger) : BaseRepository<Staff>(context, logger), IStaffRepository
    {
    }
}
