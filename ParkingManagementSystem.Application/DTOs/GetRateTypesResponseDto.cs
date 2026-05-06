namespace ParkingManagementSystem.Application.DTOs
{
    public class GetRateTypesResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsHourly { get; set; }
        public bool IsActive { get; set; }
    }
}
