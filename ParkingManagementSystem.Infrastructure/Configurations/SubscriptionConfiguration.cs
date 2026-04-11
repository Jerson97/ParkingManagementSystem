using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate)
                .IsRequired();

            builder.HasIndex(s => s.VehicleId)
                .IsUnique();

            builder.HasIndex(s => s.ParkingSpaceId)
                .IsUnique();

            builder.HasOne(s => s.Vehicle)
                .WithOne(v => v.Subscription)
                .HasForeignKey<Subscription>(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.RateType)
                .WithMany(rt => rt.Subscriptions)
                .HasForeignKey(s => s.RateTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ParkingSpace)
                .WithOne(ps => ps.Subscription)
                .HasForeignKey<Subscription>(s => s.ParkingSpaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}