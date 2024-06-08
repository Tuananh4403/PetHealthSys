using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareSystem.Data.Entites;
namespace PetCareSystem.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name
            builder.ToTable("Users");

            // Primary key
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();


            // Properties
            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.FirstName)
                   .HasMaxLength(255);

            builder.Property(u => u.LastName)
                   .HasMaxLength(255);

            builder.Property(u => u.Email)
                   .HasMaxLength(255);

            // Relationships
            builder.HasMany(u => u.UserRoles)
                   .WithOne(ur => ur.User)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
