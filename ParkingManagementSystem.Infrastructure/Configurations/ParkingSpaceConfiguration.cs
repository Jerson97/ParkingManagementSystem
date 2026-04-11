using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ParkingSpaceConfiguration : IEntityTypeConfiguration<ParkingSpace>
    {
        public void Configure(EntityTypeBuilder<ParkingSpace> builder)
        {
            builder.ToTable("ParkingSpaces");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.SpaceNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(ps => ps.SpaceNumber)
                .IsUnique();

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(ps => ps.Subscription)
                .WithOne(s => s.ParkingSpace)
                .HasForeignKey<Subscription>(s => s.ParkingSpaceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ps => ps.ParkingEntries)
                .WithOne(pe => pe.ParkingSpace)
                .HasForeignKey(pe => pe.ParkingSpaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}