using GymEquipment.Domain.History;
using GymEquipment.Domain.Products;

namespace GymEquipment.Api.Contracts;

public static class ProductMappingExtensions
{
    public static ProductDto ToDto(this Product product)
        => new(
            product.Id,
            product.Name,
            product.Description,
            product.Type,
            product.Category,
            product.WeightKg,
            product.QuantityAvailable,
            product.Price);

    public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products)
        => products.Select(p => p.ToDto());

    public static ProductHistoryEntryDto ToDto(this ProductHistoryEntry entry)
        => new(
            entry.Id,
            entry.ProductId,
            entry.ChangeType,
            entry.ChangedAtUtc,
            entry.ChangedBy,
            entry.OldValue,
            entry.NewValue);

    public static IEnumerable<ProductHistoryEntryDto> ToDto(this IEnumerable<ProductHistoryEntry> entries)
        => entries.Select(e => e.ToDto());
}
