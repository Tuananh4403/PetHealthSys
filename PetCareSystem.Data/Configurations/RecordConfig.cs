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
            builder.ToTable("Records");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.DoctorId);
            builder.Property(x=>x.Conclude);
            builder.Property(x=>x.DetailPrediction);
            builder.Property(x=>x.PetId);
            builder.HasOne(t => t.Pet).WithMany(t => t.Records)
                .HasForeignKey(t => t.PetId);
        }
    }
}
