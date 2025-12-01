using GymEquipment.Domain.ForbiddenPhrases;
using GymEquipment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                //////////////////////////////////////////////////////////////////////////////

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<GymEquipmentDbContext>();

                // SEED tylko dla testów
                if (!db.ForbiddenPhrases.Any())
                {
                    db.ForbiddenPhrases.AddRange(
                        new ForbiddenPhrase(Guid.NewGuid(), "scam"),
                        new ForbiddenPhrase(Guid.NewGuid(), "fake"),
                        new ForbiddenPhrase(Guid.NewGuid(), "illegal"));
                    db.SaveChanges();
                }
            });
        }
    }
}
