using GymEquipment.Domain.Products;

namespace GymEquipment.Api.Contracts
{
    public record UpdateProductRequest(
    string Name,
    string? Description,
    EquipmentType Type,
    ProductCategory Category,
    decimal? WeightKg,
    int QuantityAvailable,
    decimal Price);
}
