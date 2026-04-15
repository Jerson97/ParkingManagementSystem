using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Repositories
{
    public class ParkingEntryRepository : IParkingEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public ParkingEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ParkingEntry parkingEntry, CancellationToken cancellationToken)
        {
            await _context.ParkingEntries.AddAsync(parkingEntry, cancellationToken);
        }
    }
}