using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Repositories
{
    public class RateTypeRepository : IRateTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public RateTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RateType?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.RateTypes
                .FirstOrDefaultAsync(rt => rt.Id == id, cancellationToken);
        }
    }
}