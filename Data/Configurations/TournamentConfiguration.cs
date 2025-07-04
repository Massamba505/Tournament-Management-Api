using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.Enums;

namespace Tournament.Management.API.Data.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
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
                
            // Add indexes for common queries
            builder.HasIndex(t => t.StartDate);
            builder.HasIndex(t => t.IsPublic);
            builder.HasIndex(t => t.Format);
            
            // Configure enum properties
            builder.Property(t => t.Format)
                .HasConversion<string>();
                
            builder.Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}
