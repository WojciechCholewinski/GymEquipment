using GymEquipment.Domain.ForbiddenPhrases;
using GymEquipment.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymEquipment.Infrastructure.Persistence.Seed
{
    public static class ProductsSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GymEquipmentDbContext>();

            await context.Database.MigrateAsync();

            if (await context.Products.AnyAsync())
                return;

            var products = new[]
            {
                new Product(
                    Guid.NewGuid(),
                    "Olympic Barbell 20kg",
                    EquipmentType.Barbell,
                    ProductCategory.StrengthEquipment,
                    20m,
                    499m,
                    10,
                    "Standardowa olimpijska sztanga 220cm, 20kg"),

                new Product(
                    Guid.NewGuid(),
                    "Olympic Barbell 15kg",
                    EquipmentType.Barbell,
                    ProductCategory.StrengthEquipment,
                    15m,
                    459m,
                    6,
                    "Lżejsza sztanga olimpijska 15kg, idealna dla początkujących"),

                new Product(
                    Guid.NewGuid(),
                    "Hex Dumbbell 10kg",
                    EquipmentType.Dumbbell,
                    ProductCategory.StrengthEquipment,
                    10m,
                    129m,
                    20,
                    "Hantla sześciokątna 10kg, ogumowana"),

                new Product(
                    Guid.NewGuid(),
                    "Hex Dumbbell 5kg",
                    EquipmentType.Dumbbell,
                    ProductCategory.StrengthEquipment,
                    5m,
                    89m,
                    24,
                    "Hantla sześciokątna 5kg, ogumowana"),

                new Product(
                    Guid.NewGuid(),
                    "Bumper Plate 5kg",
                    EquipmentType.Plate,
                    ProductCategory.StrengthEquipment,
                    5m,
                    59m,
                    30,
                    "Talerz bumper 5kg do podnoszenia ciężarów"),

                new Product(
                    Guid.NewGuid(),
                    "Bumper Plate 10kg",
                    EquipmentType.Plate,
                    ProductCategory.StrengthEquipment,
                    10m,
                    99m,
                    20,
                    "Talerz bumper 10kg do podnoszenia ciężarów"),

                new Product(
                    Guid.NewGuid(),
                    "Flat Bench",
                    EquipmentType.Bench,
                    ProductCategory.StrengthEquipment,
                    null,
                    799m,
                    5,
                    "Ławeczka płaska do wyciskania na klatkę"),

                new Product(
                    Guid.NewGuid(),
                    "Adjustable Bench",
                    EquipmentType.Bench,
                    ProductCategory.StrengthEquipment,
                    null,
                    1299m,
                    4,
                    "Regulowana ławeczka do wyciskania i ćwiczeń pomocniczych"),

                new Product(
                    Guid.NewGuid(),
                    "Lat Pulldown Machine",
                    EquipmentType.Machine,
                    ProductCategory.StrengthEquipment,
                    null,
                    8999m,
                    2,
                    "Maszyna do ściągania drążka wyciągu górnego"),

                new Product(
                    Guid.NewGuid(),
                    "Treadmill Pro 3000",
                    EquipmentType.Machine,
                    ProductCategory.CardioEquipment,
                    null,
                    7999m,
                    2,
                    "Profesjonalna bieżnia elektryczna do siłowni"),

                new Product(
                    Guid.NewGuid(),
                    "Air Bike X",
                    EquipmentType.Machine,
                    ProductCategory.CardioEquipment,
                    null,
                    3999m,
                    3,
                    "Rower powietrzny do treningu interwałowego HIIT"),

                new Product(
                    Guid.NewGuid(),
                    "Lifting Straps",
                    EquipmentType.LiftingStraps,
                    ProductCategory.Accessories,
                    null,
                    49m,
                    25,
                    "Paski do podnoszenia ciężarów"),

                new Product(
                    Guid.NewGuid(),
                    "Weightlifting Belt",
                    EquipmentType.LiftingStraps,
                    ProductCategory.Accessories,
                    null,
                    199m,
                    15,
                    "Pas do podnoszenia ciężarów, skórzany")
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

        }
    }
}
