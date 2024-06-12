using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Table name
            builder.ToTable("Bookings");

            // Primary key
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(b => b.Number)
                   .IsRequired(false);

            builder.Property(b => b.Total)
                   .IsRequired();

            builder.Property(b => b.BookingTime)
                   .IsRequired(false);

            builder.Property(b => b.Status)
                   .HasMaxLength(50);

            // Relationships
            builder.HasOne(b => b.Customer)
                   .WithMany(c => c.Bookings)
                   .HasForeignKey(b => b.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Pet)
                   .WithMany(p => p.Bookings)
                   .HasForeignKey(b => b.PetId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Staff)
                   .WithMany(s => s.Bookings)
                   .HasForeignKey(b => b.StaffId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.BookingServices)
                   .WithOne(bs => bs.Booking)
                   .HasForeignKey(bs => bs.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
