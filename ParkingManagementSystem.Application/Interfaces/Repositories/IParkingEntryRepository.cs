using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IParkingEntryRepository
    {
        Task AddAsync(ParkingEntry parkingEntry, CancellationToken cancellationToken);
        Task<bool> ExistsActiveEntryByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
    }
}