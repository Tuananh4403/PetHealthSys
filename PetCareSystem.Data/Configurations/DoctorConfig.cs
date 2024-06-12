using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Table name
            builder.ToTable("Doctors");

            // Primary key
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();


            // Properties
            builder.Property(d => d.UserId)
                   .IsRequired();

            // Relationships
            builder.HasMany(d => d.Records)
                   .WithOne(r => r.Doctor)
                   .HasForeignKey(r => r.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
