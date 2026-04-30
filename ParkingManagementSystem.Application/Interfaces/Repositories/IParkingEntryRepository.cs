using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Repositories
{
    public interface IParkingEntryRepository
    {
        Task AddAsync(ParkingEntry parkingEntry, CancellationToken cancellationToken);
        Task<bool> ExistsActiveEntryByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
        Task<ParkingEntry?> GetByTicketNumberAsync(string ticketNumber, CancellationToken cancellationToken);
        void Update(ParkingEntry parkingEntry);
        Task<decimal> GetTodayRevenueAsync(DateTime date, CancellationToken cancellationToken);
        Task<int> CountClosedTodayAsync(DateTime date, CancellationToken cancellationToken);
        Task<List<ParkingEntry>> GetHistoryAsync(string? licensePlate, DateTime? from, DateTime? to, CancellationToken cancellationToken);
    }
}