using ParkingManagementSystem.Domain.Enum;

namespace ParkingManagementSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
