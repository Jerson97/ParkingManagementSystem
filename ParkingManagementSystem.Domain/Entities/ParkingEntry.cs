using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Domain.Entities
{
    public class ParkingEntry
    {
        public int Id { get; set; }

        public string TicketNumber { get; set; } = null!;
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        public decimal? TotalAmount { get; set; }
        public ParkingEntryStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public int RateTypeId { get; set; }
        public RateType RateType { get; set; } = null!;

        public int ParkingSpaceId { get; set; }
        public ParkingSpace ParkingSpace { get; set; } = null!;
    }
}