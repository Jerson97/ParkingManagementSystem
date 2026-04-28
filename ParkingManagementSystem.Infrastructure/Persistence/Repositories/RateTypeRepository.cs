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

        public async Task<List<RateType>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.RateTypes
                .OrderBy(rt => rt.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.RateTypes
                .AnyAsync(rt => rt.Name == name, cancellationToken);
        }

        public async Task AddAsync(RateType rateType, CancellationToken cancellationToken)
        {
            await _context.RateTypes.AddAsync(rateType, cancellationToken);
        }

        public void Update(RateType rateType)
        {
            _context.RateTypes.Update(rateType);
        }
    }
}