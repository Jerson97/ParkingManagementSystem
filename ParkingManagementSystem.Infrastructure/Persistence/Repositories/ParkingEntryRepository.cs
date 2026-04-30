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

        public async Task<decimal> GetTodayRevenueAsync(DateTime date, CancellationToken cancellationToken)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _context.ParkingEntries
                .Where(pe =>
                    pe.Status == ParkingEntryStatus.Completed &&
                    pe.ExitTime >= startDate &&
                    pe.ExitTime < endDate)
                .SumAsync(pe => pe.TotalAmount ?? 0, cancellationToken);
        }

        public async Task<int> CountClosedTodayAsync(DateTime date, CancellationToken cancellationToken)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _context.ParkingEntries
                .CountAsync(pe =>
                    pe.Status == ParkingEntryStatus.Completed &&
                    pe.ExitTime >= startDate &&
                    pe.ExitTime < endDate,
                    cancellationToken);
        }

        public async Task<List<ParkingEntry>> GetHistoryAsync(string? licensePlate, DateTime? from, DateTime? to, CancellationToken cancellationToken)
        {
            var query = _context.ParkingEntries
                .Include(pe => pe.Vehicle)
                .Include(pe => pe.ParkingSpace)
                .Include(pe => pe.RateType)
                .Where(pe => pe.Status == ParkingEntryStatus.Completed)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(licensePlate))
            {
                query = query.Where(pe => pe.Vehicle.LicensePlate == licensePlate);
            }

            if (from.HasValue)
            {
                query = query.Where(pe => pe.ExitTime >= from.Value.Date);
            }

            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                query = query.Where(pe => pe.ExitTime < toDate);
            }

            return await query
                .OrderByDescending(pe => pe.ExitTime)
                .ToListAsync(cancellationToken);
        }
    }
}