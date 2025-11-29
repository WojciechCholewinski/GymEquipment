using GymEquipment.Application.Products;
using GymEquipment.Infrastructure.Persistence;
using GymEquipment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymEquipment.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<GymEquipmentDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IForbiddenPhraseRepository, ForbiddenPhraseRepository>();
            services.AddScoped<IProductHistoryRepository, ProductHistoryRepository>();

            return services;
        }
    }
}
