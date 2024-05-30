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
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");

            builder.HasKey(t => new {t.CustomerId, t.PetId, t.ServiceId, t.StaffId});
            builder.Property(t => t.BookingId).ValueGeneratedOnAdd();

            builder.HasOne(t => t.Customer).WithMany(bk => bk.Bookings)
                .HasForeignKey(bk => bk.CustomerId);

            builder.HasOne(t => t.Pet).WithMany(bk => bk.Bookings)
                .HasForeignKey(bk => bk.PetId);

            builder.HasOne(t => t.Service).WithMany(bk => bk.Bookings)
                .HasForeignKey(bk => bk.ServiceId);

            builder.HasOne(t => t.Staff).WithMany(bk => bk.Bookings)
                .HasForeignKey(bk => bk.StaffId);
            
        }
    }
}
