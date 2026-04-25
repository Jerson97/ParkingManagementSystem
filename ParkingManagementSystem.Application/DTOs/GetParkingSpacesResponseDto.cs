namespace ParkingManagementSystem.Application.DTOs
{
    public class GetParkingSpacesResponseDto
    {
        public int Id { get; set; }
        public string SpaceNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? LicensePlate { get; set; }
        public string? CustomerName { get; set; }
    }
}
