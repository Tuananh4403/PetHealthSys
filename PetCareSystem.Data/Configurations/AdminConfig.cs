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
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasKey(x => x.AdminId);
            builder.Property(x => x.AdminId).ValueGeneratedOnAdd();

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(250);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(250).IsUnicode(true);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(250).IsUnicode(true);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);

            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
        }
    }
}
