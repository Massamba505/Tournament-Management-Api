using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;

namespace Tournament.Management.API.Data.Configurations;

public class UserTournamentConfiguration : IEntityTypeConfiguration<UserTournament>
{
    public void Configure(EntityTypeBuilder<UserTournament> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(t => t.Description)
            .IsRequired();
            
        builder.Property(t => t.Location)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(t => t.EntryFee)
            .HasPrecision(10, 2);
            
        builder.HasOne(t => t.Organizer)
            .WithMany()
            .HasForeignKey(t => t.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Property(t => t.Format)
            .HasConversion<string>();
            
        builder.Property(t => t.Status)
            .HasConversion<string>();
    }
}
