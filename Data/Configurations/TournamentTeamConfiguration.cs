using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Data.Configurations
{
    public class TournamentTeamConfiguration : IEntityTypeConfiguration<TournamentTeam>
    {
        public void Configure(EntityTypeBuilder<TournamentTeam> builder)
        {
            // Use composite key
            builder.HasKey(tt => new { tt.TournamentId, tt.TeamId });
            
            builder.HasOne(tt => tt.Tournament)
                .WithMany(t => t.TournamentTeams)
                .HasForeignKey(tt => tt.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(tt => tt.Team)
                .WithMany()
                .HasForeignKey(tt => tt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configure enum property
            builder.Property(tt => tt.Status)
                .HasConversion<string>();
        }
    }
}
