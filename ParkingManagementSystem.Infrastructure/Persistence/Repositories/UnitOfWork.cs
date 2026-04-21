using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IVehicleRepository Vehicles { get; }
        public IRateTypeRepository RateTypes { get; }
        public IParkingSpaceRepository ParkingSpaces { get; }
        public IParkingEntryRepository ParkingEntries { get; }

        public ISubscriptionRepository Subscriptions { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IVehicleRepository vehicleRepository,
            IRateTypeRepository rateTypeRepository,
            IParkingSpaceRepository parkingSpaceRepository,
            IParkingEntryRepository parkingEntryRepository,
            ISubscriptionRepository subscriptions)
        {
            _context = context;
            Vehicles = vehicleRepository;
            RateTypes = rateTypeRepository;
            ParkingSpaces = parkingSpaceRepository;
            ParkingEntries = parkingEntryRepository;
            Subscriptions = subscriptions;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}