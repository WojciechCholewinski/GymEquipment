using GymEquipment.Domain.Products;

namespace GymEquipment.Api.Contracts
{
    public record UpdateProductRequest(
    string Name,
    decimal? WeightKg,
    int QuantityAvailable,
    decimal Price,
    string? Description);
}
