using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Configurations
{
    public class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Service");

            builder.HasKey(x => x.ServiceId);
            builder.Property(x =>x.ServiceId).ValueGeneratedOnAdd();

            builder.Property(x => x.TypeOfService).IsRequired().HasMaxLength(500).IsUnicode(true);

            builder.Property(x => x.ServiceName).IsRequired().HasMaxLength(300).IsUnicode(true);

            builder.Property(x => x.Price).IsRequired();
        }
    }
}
