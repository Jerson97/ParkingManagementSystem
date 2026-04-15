using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IVehicleRepository Vehicles { get; }
        public IParkingSpaceRepository ParkingSpaces { get; }
        public IParkingEntryRepository ParkingEntries { get; }
        public IRateTypeRepository RateTypes { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IVehicleRepository vehicleRepository,
            IParkingSpaceRepository parkingSpaceRepository,
            IParkingEntryRepository parkingEntryRepository,
            IRateTypeRepository rateTypeRepository)
        {
            _context = context;
            Vehicles = vehicleRepository;
            ParkingSpaces = parkingSpaceRepository;
            ParkingEntries = parkingEntryRepository;
            RateTypes = rateTypeRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}