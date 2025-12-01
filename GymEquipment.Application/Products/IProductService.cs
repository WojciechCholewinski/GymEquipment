using GymEquipment.Application.Common;
using GymEquipment.Domain.History;
using GymEquipment.Domain.Products;

namespace GymEquipment.Application.Products
{
    public interface IProductService
    {
        Task<(ValidationResult Validation, Product? Product)> CreateAsync(
            string name,
            EquipmentType type,
            ProductCategory category,
            decimal? weightKg,
            int quantity,
            decimal price,
            string? description,
            CancellationToken cancellationToken = default);

        Task<(ValidationResult Validation, Product? Product)> UpdateAsync(
            Guid id,
            string name,
            int quantity,
            decimal price,
            string? description,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProductHistoryEntry>> GetHistoryAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
