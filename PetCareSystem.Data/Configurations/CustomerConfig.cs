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

            // Primary key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId).IsRequired();
            // Relationships
            builder.HasMany(c => c.Pets)
                   .WithOne(p => p.Customer)
                   .HasForeignKey(p => p.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Bookings)
                   .WithOne(b => b.Customer)
                   .HasForeignKey(b => b.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
