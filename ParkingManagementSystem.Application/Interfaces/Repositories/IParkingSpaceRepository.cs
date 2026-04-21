using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IParkingSpaceRepository
    {
        Task<ParkingSpace?> GetAvailableSpaceAsync(CancellationToken cancellationToken);
        Task<ParkingSpace?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void Update(ParkingSpace parkingSpace);
    }
}