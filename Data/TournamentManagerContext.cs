using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Models.Domain;

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

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<TournamentFormat> TournamentFormats => Set<TournamentFormat>();
    public DbSet<UserTournament> UserTournaments => Set<UserTournament>();
    public DbSet<TournamentTeam> TournamentTeams => Set<TournamentTeam>();
    public DbSet<TeamMatch> TeamMatches => Set<TeamMatch>();
    public DbSet<PlayerStat> PlayerStats => Set<PlayerStat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "General" },
            new Role { Id = 2, Name = "Organizer" }
        );

        modelBuilder.Entity<TournamentFormat>().HasData(
            new TournamentFormat { Id = 1, Name = "Single Elimination" },
            new TournamentFormat { Id = 2, Name = "Double Elimination" },
            new TournamentFormat { Id = 3, Name = "Round Robin" }
        );

        modelBuilder.Entity<Member>().HasData(
            new Member { Id = 1, Name = "Player"},
            new Member { Id = 2, Name = "Manager" }
        );

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Manager)
            .WithMany(u => u.ManagedTeams)
            .HasForeignKey(t => t.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Captain)
            .WithMany(u => u.CaptainedTeams)
            .HasForeignKey(t => t.CaptainId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeamMember>()
            .HasOne(tm => tm.User)
            .WithMany(u => u.TeamMemberships)
            .HasForeignKey(tm => tm.UserId);

        modelBuilder.Entity<TeamMember>()
            .HasOne(tm => tm.Team)
            .WithMany(t => t.Members)
            .HasForeignKey(tm => tm.TeamId);

        modelBuilder.Entity<UserTournament>()
            .HasOne(t => t.Format)
            .WithMany()
            .HasForeignKey(t => t.FormatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserTournament>()
            .HasOne(t => t.Organizer)
            .WithMany()
            .HasForeignKey(t => t.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TournamentTeam>()
            .HasIndex(tt => new { tt.TournamentId, tt.TeamId })
            .IsUnique();

        modelBuilder.Entity<UserTournament>()
            .Property(ut => ut.EntryFee)
            .HasPrecision(10, 2);

        modelBuilder.Entity<TeamMatch>()
            .HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeamMatch>()
            .HasOne(m => m.AwayTeam)
            .WithMany()
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeamMatch>()
            .HasOne(m => m.Tournament)
            .WithMany()
            .HasForeignKey(m => m.TournamentId);

        modelBuilder.Entity<PlayerStat>()
            .HasIndex(ps => new { ps.PlayerId, ps.MatchId })
            .IsUnique();
    }

}
