using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Configurations
{
    public class RateTypeConfiguration : IEntityTypeConfiguration<RateType>
    {
        public void Configure(EntityTypeBuilder<RateType> builder)
        {
            builder.ToTable("RateTypes");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.IsActive)
                .IsRequired();

            builder.Property(rt => rt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(rt => rt.Name)
                .IsUnique();

            builder.Property(rt => rt.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(rt => rt.IsHourly)
                .IsRequired();

            builder.HasMany(rt => rt.Subscriptions)
                .WithOne(s => s.RateType)
                .HasForeignKey(s => s.RateTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(rt => rt.ParkingEntries)
                .WithOne(pe => pe.RateType)
                .HasForeignKey(pe => pe.RateTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}