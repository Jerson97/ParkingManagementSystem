namespace ParkingManagementSystem.Domain.Entities
{
    public class RateType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsHourly { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public ICollection<ParkingEntry> ParkingEntries { get; set; } = new List<ParkingEntry>();
    }
}
