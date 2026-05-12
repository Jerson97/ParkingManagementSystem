namespace ParkingManagementSystem.Application.DTOs
{
    public class CreateParkingEntryResponseDto
    {
        public int ParkingEntryId { get; set; }
        public string TicketNumber { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public string RateTypeName { get; set; } = null!;
        public DateTime EntryTime { get; set; }
        public string SpaceNumber { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public decimal RateTypePrice { get; set; }
        public bool IsHourly { get; set; }
        public string Status { get; set; } = null!;
    }
}
