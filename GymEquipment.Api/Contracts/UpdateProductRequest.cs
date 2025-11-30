using GymEquipment.Domain.Products;

namespace GymEquipment.Api.Contracts
{
    public record UpdateProductRequest(
    decimal? WeightKg,
    int QuantityAvailable,
    decimal Price,
    string? Description);
}
