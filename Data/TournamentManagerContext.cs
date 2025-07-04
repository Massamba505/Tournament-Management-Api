using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Data.Configurations;

namespace Tournament.Management.API.Data;

public partial class TournamentManagerContext : DbContext
{
    public TournamentManagerContext()
    {
    }

    public TournamentManagerContext(DbContextOptions<TournamentManagerContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Tournament.Management.API.Models.Domain.Tournament> Tournaments => Set<Tournament.Management.API.Models.Domain.Tournament>();
    public DbSet<TournamentTeam> TournamentTeams => Set<TournamentTeam>();
    public DbSet<TeamMatch> TeamMatches => Set<TeamMatch>();
    public DbSet<PlayerStat> PlayerStats => Set<PlayerStat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from separate classes
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMemberConfiguration());
        modelBuilder.ApplyConfiguration(new TournamentConfiguration());
        modelBuilder.ApplyConfiguration(new TournamentTeamConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMatchConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerStatConfiguration());
    }
}
