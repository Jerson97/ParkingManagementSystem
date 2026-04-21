using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Infrastructure.Persistence.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken)
        {
            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
        }

        public async Task<bool> ExistsByParkingSpaceIdAsync(int parkingSpaceId, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .AnyAsync(s => s.ParkingSpaceId == parkingSpaceId, cancellationToken);
        }

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .Include(s => s.Vehicle)
                .AnyAsync(s => s.Vehicle.LicensePlate == licensePlate, cancellationToken);
        }
    }
}