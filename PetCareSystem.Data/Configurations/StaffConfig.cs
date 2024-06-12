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
            // Table name
            builder.ToTable("Staff");

            // Primary key
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();


            builder.Property(s => s.UserId).IsRequired();
            // Relationship with Bookings
            builder.HasMany(s => s.Bookings)
                   .WithOne(b => b.Staff)
                   .HasForeignKey(b => b.StaffId)
                   .OnDelete(DeleteBehavior.Restrict); // Set null if the staff is deleted

            // Other configurations if needed
        }
    }
}
