using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Domain.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public string SpaceNumber { get; set; } = null!;
        public ParkingSpaceStatus Status { get; set; }

        public Subscription? Subscription { get; set; }
        public ICollection<ParkingEntry> ParkingEntries { get; set; } = new List<ParkingEntry>();
    }
}
