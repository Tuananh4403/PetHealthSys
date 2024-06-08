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
    public class RecordConfig : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            // Table name
            builder.ToTable("Records");

            // Primary key
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();


            // Properties
            builder.Property(r => r.saveBarn)
                   .IsRequired();

            builder.Property(r => r.DetailPrediction)
                   .HasMaxLength(1000);

            builder.Property(r => r.Conclude)
                   .HasMaxLength(1000);

            // Relationships
            builder.HasOne(r => r.Doctor)
                   .WithMany(d => d.Records)
                   .HasForeignKey(r => r.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Pet)
                   .WithMany(p => p.Records)
                   .HasForeignKey(r => r.PetId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.RecordDetails)
                   .WithOne(rd => rd.Record)
                   .HasForeignKey(rd => rd.RecordId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
