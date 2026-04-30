namespace ParkingManagementSystem.Application.DTOs
{
    public class GetDashboardSummaryResponseDto
    {
        public int TotalSpaces { get; set; }
        public int AvailableSpaces { get; set; }
        public int OccupiedSpaces { get; set; }
        public int ReservedSpaces { get; set; }

        public int ActiveSubscriptions { get; set; }
        public int ExpiredSubscriptions { get; set; }

        public decimal TodayRevenue { get; set; }
        public int ClosedTicketsToday { get; set; }
    }
}
