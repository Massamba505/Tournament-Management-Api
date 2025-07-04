using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.HasIndex(u => u.Email)
            .IsUnique();
            
        builder.Property(u => u.Role)
            .HasConversion<string>();
            
        builder.Property(u => u.Status)
            .HasConversion<string>();
    }
}
