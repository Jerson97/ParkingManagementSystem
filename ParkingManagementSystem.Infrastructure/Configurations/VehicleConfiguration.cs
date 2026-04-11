using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(v => v.LicensePlate)
                .IsUnique();

            builder.HasMany(v => v.ParkingEntries)
                .WithOne(pe => pe.Vehicle)
                .HasForeignKey(pe => pe.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Subscription)
                .WithOne(s => s.Vehicle)
                .HasForeignKey<Subscription>(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}