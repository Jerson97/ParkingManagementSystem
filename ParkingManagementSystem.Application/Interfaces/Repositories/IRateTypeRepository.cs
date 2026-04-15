using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IRateTypeRepository
    {
        Task<RateType?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}