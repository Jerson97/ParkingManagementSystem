using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public SubscriptionStatus Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public int RateTypeId { get; set; }
        public RateType RateType { get; set; } = null!;

        public int ParkingSpaceId { get; set; }
        public ParkingSpace ParkingSpace { get; set; } = null!;
    }
}