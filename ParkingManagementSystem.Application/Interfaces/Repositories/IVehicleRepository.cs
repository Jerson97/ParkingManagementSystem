using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    }
}
