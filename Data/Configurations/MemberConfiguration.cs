using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Data.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
                
            // Seed data
            builder.HasData(
                new Member { Id = 1, Name = "Player" },
                new Member { Id = 2, Name = "Manager" }
            );
        }
    }
}
