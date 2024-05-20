using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetHealthSys.Entity;

namespace PetHealthSys.Data
{
    public class PetHealthSysContext : DbContext
    {
        public PetHealthSysContext (DbContextOptions<PetHealthSysContext> options)
            : base(options)
        {
        }

        public DbSet<PetHealthSys.Entity.User> User { get; set; } = default!;
    }
}
