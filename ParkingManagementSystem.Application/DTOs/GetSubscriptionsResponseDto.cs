namespace ParkingManagementSystem.Application.DTOs
{
    public class GetSubscriptionsResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public string RateTypeName { get; set; } = null!;
        public string SpaceNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}