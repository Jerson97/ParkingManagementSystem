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

        public void Update(ParkingSpace parkingSpace)
        {
            _context.ParkingSpaces.Update(parkingSpace);
        }
    }
}