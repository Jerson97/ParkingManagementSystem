namespace ParkingManagementSystem.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = null!;

        public ICollection<ParkingEntry> ParkingEntries { get; set; } = new List<ParkingEntry>();
        public Subscription? Subscription { get; set; }
    }
}