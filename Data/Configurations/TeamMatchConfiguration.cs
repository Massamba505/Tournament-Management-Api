using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Data.Configurations;

public class TeamMatchConfiguration : IEntityTypeConfiguration<TeamMatch>
{
    public void Configure(EntityTypeBuilder<TeamMatch> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Venue)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.HasOne(m => m.Tournament)
            .WithMany(t => t.Matches)
            .HasForeignKey(m => m.TournamentId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(m => m.AwayTeam)
            .WithMany()
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(m => m.Status)
            .HasConversion<string>();
    }
}
