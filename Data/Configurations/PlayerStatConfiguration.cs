using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Data.Configurations
{
    public class PlayerStatConfiguration : IEntityTypeConfiguration<PlayerStat>
    {
        public void Configure(EntityTypeBuilder<PlayerStat> builder)
        {
            // Use composite key
            builder.HasKey(ps => new { ps.PlayerId, ps.MatchId });
            
            builder.HasOne(ps => ps.Player)
                .WithMany()
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(ps => ps.Match)
                .WithMany(m => m.PlayerStats)
                .HasForeignKey(ps => ps.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configure enum property
            builder.Property(ps => ps.Position)
                .HasConversion<string>();
        }
    }
}
