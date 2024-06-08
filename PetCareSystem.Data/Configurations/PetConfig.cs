using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class PetConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            // Table name
            builder.ToTable("Pets");

            // Primary key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            // Properties
            builder.Property(p => p.PetName)
                   .HasMaxLength(255);

            builder.Property(p => p.KindOfPet)
                   .HasMaxLength(255);

            builder.Property(p => p.Species)
                   .HasMaxLength(255);

            // Relationships
            builder.HasMany(p => p.Bookings)
                   .WithOne(b => b.Pet)
                   .HasForeignKey(b => b.PetId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Records)
                   .WithOne(r => r.Pet)
                   .HasForeignKey(r => r.PetId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
