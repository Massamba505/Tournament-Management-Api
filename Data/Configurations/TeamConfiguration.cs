using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.HasIndex(t => t.Name);
                
            builder.HasOne(t => t.Manager)
                .WithMany(u => u.ManagedTeams)
                .HasForeignKey(t => t.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(t => t.Captain)
                .WithMany(u => u.CaptainedTeams)
                .HasForeignKey(t => t.CaptainId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configure enum property
            builder.Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}
