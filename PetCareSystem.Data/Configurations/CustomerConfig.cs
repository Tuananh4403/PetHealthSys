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
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).ValueGeneratedOnAdd();

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(250).IsUnicode(true);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(250).IsUnicode(true);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(500).IsUnicode(true);

            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();

        }
    }
}
