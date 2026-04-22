using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task AddAsync(Subscription subscription, CancellationToken cancellationToken);
        Task<bool> ExistsByParkingSpaceIdAsync(int parkingSpaceId, CancellationToken cancellationToken);
        Task<bool> ExistsByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
        Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Subscription>> GetExpiredSubscriptionsAsync(DateTime currentDate, CancellationToken cancellationToken);
    }
}