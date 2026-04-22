namespace ParkingManagementSystem.Application.DTOs
{
    public class ProcessExpiredSubscriptionsResponseDto
    {
        public int SubscriptionId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public string SpaceNumber { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public string NewStatus { get; set; } = null!;
    }
}
