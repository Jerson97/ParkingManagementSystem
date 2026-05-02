using ParkingManagementSystem.Domain.Entities;

namespace ParkingManagementSystem.Application.Interfaces.Token
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
