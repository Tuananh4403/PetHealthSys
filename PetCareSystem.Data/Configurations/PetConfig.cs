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
    public class PetConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pet");

            builder.HasKey(p => p.PetId);
            builder.Property(p => p.PetId).ValueGeneratedOnAdd();

            builder.Property(p => p.PetName)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(true);

            builder.Property(p => p.KindOfPet)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(true);

            builder.Property(p => p.Gender)
               .IsRequired();

            builder.Property(p => p.Birthday)
                .IsRequired();

            builder.Property(p => p.Species)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(true);
        }
    }
}
