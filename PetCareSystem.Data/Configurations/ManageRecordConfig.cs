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
    public class ManageRecordConfig : IEntityTypeConfiguration<ManageRecord>
    {
        public void Configure(EntityTypeBuilder<ManageRecord> builder)
        {
            builder.ToTable("ManageRecord");

            builder.HasKey(t => new {t.DoctorId, t.RecordId});
            builder.Property(t => t.ManageId).ValueGeneratedOnAdd();

            builder.HasOne(t => t.Doctor).WithMany(bk => bk.ManageRecords)
                .HasForeignKey(bk => bk.DoctorId);

            builder.HasOne(t => t.Record).WithMany(bk => bk.ManageRecords)
                .HasForeignKey(bk => bk.RecordId);
            
        }
    }
}
