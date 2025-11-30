using GymEquipment.Domain.ForbiddenPhrases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymEquipment.Infrastructure.Persistence.Seed
{
    public static class ForbiddenPhrasesSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GymEquipmentDbContext>();

            await context.Database.MigrateAsync();

            if (await context.ForbiddenPhrases.AnyAsync())
                return;

            var phrases = new[]
            {
                new ForbiddenPhrase(Guid.NewGuid(), "scam"),
                new ForbiddenPhrase(Guid.NewGuid(), "fake"),
                new ForbiddenPhrase(Guid.NewGuid(), "illegal"),
                new ForbiddenPhrase(Guid.NewGuid(), "banned"),
                new ForbiddenPhrase(Guid.NewGuid(), "counterfeit"),
                new ForbiddenPhrase(Guid.NewGuid(), "stolen"),
                new ForbiddenPhrase(Guid.NewGuid(), "pirated"),
                new ForbiddenPhrase(Guid.NewGuid(), "unauthorized"),
                new ForbiddenPhrase(Guid.NewGuid(), "fraud"),
                new ForbiddenPhrase(Guid.NewGuid(), "cheat"),
                new ForbiddenPhrase(Guid.NewGuid(), "spam"),
                new ForbiddenPhrase(Guid.NewGuid(), "phishing"),
                new ForbiddenPhrase(Guid.NewGuid(), "exploit"),
                new ForbiddenPhrase(Guid.NewGuid(), "malware"),
                new ForbiddenPhrase(Guid.NewGuid(), "unsafe"),
                new ForbiddenPhrase(Guid.NewGuid(), "black market")
            };
            await context.ForbiddenPhrases.AddRangeAsync(phrases);
            await context.SaveChangesAsync();
        }
    }
}
