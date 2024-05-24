using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.EF
{
    public class PetHealthDBContextFactory : IDesignTimeDbContextFactory<PetHealthDBContext>
    {
        public PetHealthDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("PetHealthCareDb");
            var optionsBuilder = new DbContextOptionsBuilder<PetHealthDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PetHealthDBContext(optionsBuilder.Options);
        }
    }
}
