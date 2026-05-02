using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingEntry> ParkingEntries { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<RateType> RateTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }

    }
}