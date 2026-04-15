using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Infrastructure.Persistence;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate, cancellationToken);
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        await _context.Vehicles.AddAsync(vehicle, cancellationToken);
    }
}