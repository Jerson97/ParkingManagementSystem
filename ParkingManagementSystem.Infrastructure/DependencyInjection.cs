using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Infrastructure.Persistence;
using ParkingManagementSystem.Infrastructure.Persistence.Repositories;

namespace ParkingManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
            services.AddScoped<IParkingEntryRepository, ParkingEntryRepository>();
            services.AddScoped<IRateTypeRepository, RateTypeRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
