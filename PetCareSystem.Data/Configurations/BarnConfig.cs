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
    public class BarnConfig : IEntityTypeConfiguration<Barn>
    {
        public void Configure(EntityTypeBuilder<Barn> builder)
        {
            // Table name
            builder.ToTable("Barns");

            // Primary key
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();


            // Soft delete query filter
            builder.HasQueryFilter(b => b.DeletedAt == null);

            // Relationships
            builder.HasMany(b => b.Records)
                   .WithOne(r => r.Barn)
                   .HasForeignKey(r => r.BarnId);

            // Other configurations if needed
        }
    }
}
