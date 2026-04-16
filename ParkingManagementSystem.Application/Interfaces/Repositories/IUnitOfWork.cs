namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IVehicleRepository Vehicles { get; }
        IRateTypeRepository RateTypes { get; }
        IParkingSpaceRepository ParkingSpaces { get; }
        IParkingEntryRepository ParkingEntries { get; }
    }
}