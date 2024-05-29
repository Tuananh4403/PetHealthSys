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
    public class StaffConfig : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");

            builder.HasKey(x => x.StaffId);
            builder.Property(x => x.StaffId).ValueGeneratedOnAdd();

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(250);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(250).IsUnicode(true);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(250).IsUnicode(true);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            
            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();

            builder.Property(x => x.Address).IsRequired().HasMaxLength(500).IsUnicode(true);

        }
    }
}
