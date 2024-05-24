using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.Configurations;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.EF
{
    public class PetHealthDBContext : DbContext
    {
        public PetHealthDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            //base.OnModelCreating(modelBuilder);
        }

        
    }
}
