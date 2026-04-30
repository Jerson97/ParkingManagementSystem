using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Domain.Enums;

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

        public async Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .Include(s => s.Vehicle)
                .Include(s => s.RateType)
                .Include(s => s.ParkingSpace)
                .OrderBy(s => s.CustomerName)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Subscription>> GetExpiredSubscriptionsAsync(DateTime currentDate, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .Include(s => s.Vehicle)
                .Include(s => s.ParkingSpace)
                .Where(s => s.Status == SubscriptionStatus.Active && s.EndDate < currentDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Subscription?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .Include(s => s.RateType)
                .Include(s => s.ParkingSpace)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public void Update(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
        }

        public async Task<int> CountByStatusAsync(SubscriptionStatus status, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .CountAsync(s => s.Status == status, cancellationToken);
        }
    }
}