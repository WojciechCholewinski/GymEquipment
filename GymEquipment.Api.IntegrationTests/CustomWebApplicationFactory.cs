using GymEquipment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GymEquipment.Api.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Testing")
                .ConfigureServices(services =>
            {
                // Usuwamy prawdziwy DbContext (SQL Server)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<GymEquipmentDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                // Rejestrujemy EF in-memory tylko na czas testów
                services.AddDbContext<GymEquipmentDbContext>(options =>
                {
                    options.UseInMemoryDatabase("GymEquipmentTests");
                });
            });
        }
    }
}
