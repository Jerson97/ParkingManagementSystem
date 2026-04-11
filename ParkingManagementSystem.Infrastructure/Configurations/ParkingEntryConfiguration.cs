using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ParkingEntryConfiguration : IEntityTypeConfiguration<ParkingEntry>
    {
        public void Configure(EntityTypeBuilder<ParkingEntry> builder)
        {
            builder.ToTable("ParkingEntries");

            builder.HasKey(pe => pe.Id);

            builder.Property(pe => pe.TicketNumber) 
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(pe => pe.TicketNumber)
                .IsUnique();

            builder.Property(pe => pe.EntryTime)
                .IsRequired();

            builder.Property(pe => pe.ExitTime)
                .IsRequired(false);

            builder.Property(pe => pe.TotalAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired(false);

            builder.Property(pe => pe.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(pe => pe.PaymentStatus)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(pe => pe.Vehicle)
                .WithMany(v => v.ParkingEntries)
                .HasForeignKey(pe => pe.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pe => pe.RateType)
                .WithMany(rt => rt.ParkingEntries)
                .HasForeignKey(pe => pe.RateTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pe => pe.ParkingSpace)
                .WithMany(ps => ps.ParkingEntries)
                .HasForeignKey(pe => pe.ParkingSpaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}