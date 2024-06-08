using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class RecordDetailConfig : IEntityTypeConfiguration<RecordDetail>
    {
        public void Configure(EntityTypeBuilder<RecordDetail> builder)
        {
            // Table name
            builder.ToTable("RecordDetails");

            // Primary key
            builder.HasKey(rd => rd.Id);
            builder.Property(rd => rd.Id).ValueGeneratedOnAdd();


            // Properties
            builder.Property(rd => rd.Quantity);

            // Relationships
            builder.HasOne(rd => rd.Record)
                .WithMany(r => r.RecordDetails)
                .HasForeignKey(rd => rd.RecordId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rd => rd.Service)
                .WithMany(s => s.RecordDetails)
                .HasForeignKey(rd => rd.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
