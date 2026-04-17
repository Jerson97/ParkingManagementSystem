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

        public async Task<ParkingEntry?> GetByTicketNumberAsync(string ticketNumber, CancellationToken cancellationToken)
        {
            return await _context.ParkingEntries
                .Include(pe => pe.Vehicle)
                .Include(p => p.RateType)
                .Include(p => p.ParkingSpace)
                .FirstOrDefaultAsync(p => p.TicketNumber == ticketNumber, cancellationToken);
        }

        public void Update(ParkingEntry parkingEntry)
        {
            _context.ParkingEntries.Update(parkingEntry);
        }
    }
}