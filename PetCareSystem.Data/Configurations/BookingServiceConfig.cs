using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class BookingServiceConfig : IEntityTypeConfiguration<BookingService>
    {
        public void Configure(EntityTypeBuilder<BookingService> builder)
        {
            // Table name
            builder.ToTable("BookingServices");

            // Primary key
            builder.HasKey(bs => bs.Id);
            builder.Property(bs => bs.Id).ValueGeneratedOnAdd();


            // Relationships
            builder.HasOne(bs => bs.Service)
                   .WithMany(s => s.BookingServicess)
                   .HasForeignKey(bs => bs.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bs => bs.Booking)
                   .WithMany(b => b.BookingServices)
                   .HasForeignKey(bs => bs.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Other configurations if needed
        }
    }
}
