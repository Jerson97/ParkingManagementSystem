using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Domain.Enums;

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

        public async Task<bool> ExistsActiveEntryByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken)
        {
            return await _context.ParkingEntries
                .Include(p => p.Vehicle)
                .AnyAsync(p =>
                    p.Vehicle.LicensePlate == licensePlate &&
                    p.Status == ParkingEntryStatus.Inside,
                    cancellationToken);
        }
    }
}