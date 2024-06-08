using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table name
            builder.ToTable("Roles");

            // Primary key
            builder.HasKey(r => r.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();


            // Other configurations if needed
        }
    }
}
