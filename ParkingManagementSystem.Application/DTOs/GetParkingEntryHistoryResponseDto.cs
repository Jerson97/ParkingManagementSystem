namespace ParkingManagementSystem.Application.DTOs
{
    public class GetParkingEntryHistoryResponseDto
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public string SpaceNumber { get; set; } = null!;
        public string RateTypeName { get; set; } = null!;
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
