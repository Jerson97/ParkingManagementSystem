using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IRateTypeRepository
    {
        Task<RateType?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<RateType>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
        Task AddAsync(RateType rateType, CancellationToken cancellationToken);
        void Update(RateType rateType);
    }
}