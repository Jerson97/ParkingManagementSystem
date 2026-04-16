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

        public UnitOfWork(
            ApplicationDbContext context,
            IVehicleRepository vehicleRepository,
            IRateTypeRepository rateTypeRepository,
            IParkingSpaceRepository parkingSpaceRepository,
            IParkingEntryRepository parkingEntryRepository)
        {
            _context = context;
            Vehicles = vehicleRepository;
            RateTypes = rateTypeRepository;
            ParkingSpaces = parkingSpaceRepository;
            ParkingEntries = parkingEntryRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}