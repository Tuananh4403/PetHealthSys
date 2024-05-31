using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Configurations
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Table name
            builder.ToTable("UserRoles");

            // Composite primary key
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // Relationships
            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Other configurations if needed
        }
    }
}
