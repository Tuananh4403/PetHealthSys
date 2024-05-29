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

            builder.HasKey(x => new {x.RecordId, x.PetId});
            builder.Property(x => x.RecordId).ValueGeneratedOnAdd();

            builder.Property(x => x.Medicine).HasMaxLength(300).IsUnicode(true);
            builder.Property(x => x.Vaccine).HasMaxLength(300).IsUnicode(true);
            
            builder.HasOne(t => t.Pet).WithMany(t => t.Records)
                .HasForeignKey(t => t.PetId);
        }
    }
}
