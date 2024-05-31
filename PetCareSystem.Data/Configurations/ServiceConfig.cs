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
            // Table name
            builder.ToTable("Services");

            // Primary key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.TypeOfService)
                   .HasMaxLength(100);

            builder.Property(s => s.ServiceName)
                   .HasMaxLength(100);

            builder.Property(s => s.Price)
                   .IsRequired();

            builder.Property(s => s.Status)
                   .HasMaxLength(50);

            builder.Property(s => s.Note)
                   .HasMaxLength(200);

            // Relationships
            builder.HasMany(s => s.BookingServicess)
                   .WithOne(bs => bs.Service)
                   .HasForeignKey(bs => bs.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.RecordDetails)
                   .WithOne(rd => rd.Service)
                   .HasForeignKey(rd => rd.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
