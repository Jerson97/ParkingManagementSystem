using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IParkingSpaceRepository
    {
        Task<ParkingSpace?> GetAvailableSpaceAsync(CancellationToken cancellationToken);
        Task<ParkingSpace?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<ParkingSpace>> GetAllAsync(CancellationToken cancellationToken);
        void Update(ParkingSpace parkingSpace);
        Task<int> CountByStatusAsync(ParkingSpaceStatus status, CancellationToken cancellationToken);
        Task<int> CountAllAsync(CancellationToken cancellationToken);
    }
}