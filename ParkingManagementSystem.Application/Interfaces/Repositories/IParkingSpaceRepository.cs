using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IParkingSpaceRepository
    {
        Task<ParkingSpace?> GetAvailableSpaceAsync(CancellationToken cancellationToken);
        void Update(ParkingSpace parkingSpace);
    }
}