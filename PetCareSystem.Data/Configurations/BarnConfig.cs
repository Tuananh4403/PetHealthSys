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
    public class BarnConfig : IEntityTypeConfiguration<Barn>
    {
        public void Configure(EntityTypeBuilder<Barn> builder)
        {
            builder.ToTable("Barns");

            builder.HasKey(x => x.BarnId);
            builder.Property(x => x.BarnId).ValueGeneratedOnAdd();

            builder.Property(x => x.DateSaveBarn).IsRequired();

            builder.Property(x => x.Status).IsRequired().HasMaxLength(1000).IsUnicode(true);

            builder.Property(x => x.Medicine).HasMaxLength(300).IsUnicode(true);

            builder.Property(x => x.Vaccine).HasMaxLength(250).IsUnicode(true);

            builder.Property(x => x.Result).IsRequired();
        }
    }
}
