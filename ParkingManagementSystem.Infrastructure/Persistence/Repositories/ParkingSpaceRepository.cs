using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Infrastructure.Persistence.Repositories
{
    public class ParkingSpaceRepository : IParkingSpaceRepository
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ParkingSpace?> GetAvailableSpaceAsync(CancellationToken cancellationToken)
        {
            return await _context.ParkingSpaces
                .FirstOrDefaultAsync(ps => ps.Status == ParkingSpaceStatus.Available, cancellationToken);
        }

        public async Task<ParkingSpace?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.ParkingSpaces
                .FirstOrDefaultAsync(ps => ps.Id == id, cancellationToken);
        }

        public void Update(ParkingSpace parkingSpace)
        {
            _context.ParkingSpaces.Update(parkingSpace);
        }

        public async Task<List<ParkingSpace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ParkingSpaces
                .Include(ps => ps.Subscription)
                    .ThenInclude(s => s.Vehicle)
                .Include(ps => ps.ParkingEntries)
                    .ThenInclude(pe => pe.Vehicle)
                .OrderBy(ps => ps.SpaceNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ParkingSpaces
                .CountAsync(cancellationToken);
        }

        public async Task<int> CountByStatusAsync(ParkingSpaceStatus status, CancellationToken cancellationToken)
        {
            return await _context.ParkingSpaces
                .CountAsync(ps => ps.Status == status, cancellationToken);
        }
    }
}