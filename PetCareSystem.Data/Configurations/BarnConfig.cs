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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.DateStart);

            builder.Property(x => x.DateEnd);

            builder.Property(x => x.Status).HasMaxLength(1000).IsUnicode(true);
            builder.Property(x => x.IsDeleted);

            builder.Property(x => x.Result);
        }
    }
}
