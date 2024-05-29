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
    public class ManageServiceConfig : IEntityTypeConfiguration<ManageService>
    {
        public void Configure(EntityTypeBuilder<ManageService> builder)
        {
            builder.ToTable("ManageService");
            builder.HasKey(t => t.ManageId);
            builder.Property(t => t.ManageId).ValueGeneratedOnAdd();

            
            builder.HasOne(t => t.Admin).WithMany(bk => bk.ManageSevices)
                .HasForeignKey(bk => bk.AdminId);

            
            builder.HasOne(t => t.Service).WithMany(bk => bk.ManageSevices)
                .HasForeignKey(bk => bk.ServiceId);

        }
    }
}
