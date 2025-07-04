using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.HasIndex(r => r.Name)
                .IsUnique();
                
            // Seed data
            builder.HasData(
                new Role { Id = 1, Name = "General" },
                new Role { Id = 2, Name = "Organizer" }
            );
        }
    }
}
