using GymEquipment.Application.Common;
using GymEquipment.Application.Specification;
using GymEquipment.Domain.History;
using GymEquipment.Domain.Products;
using GymEquipment.Domain.Products.Specifications;

namespace GymEquipment.Application.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _products;
    private readonly IForbiddenPhraseRepository _forbiddenPhrases;
    private readonly IProductHistoryRepository _history;

    private readonly ProductPriceRangeSpecification _priceRangeSpec = new();
    private readonly ProductWeightRequiredSpecification _weightRequiredSpec = new();
    private readonly ProductNameFormatSpecification _nameFormatSpec = new();

    public ProductService(
        IProductRepository products,
        IForbiddenPhraseRepository forbiddenPhrases,
        IProductHistoryRepository history)
    {
        _products = products;
        _forbiddenPhrases = forbiddenPhrases;
        _history = history;
    }

    public async Task<(ValidationResult Validation, Product? Product)> CreateAsync(
        string name,
        EquipmentType type,
        ProductCategory category,
        decimal? weightKg,
        int quantity,
        decimal price,
        string? description,
        CancellationToken cancellationToken = default)
    {
        #region Walidacja

        var validation = new ValidationResult();

        // 1. Walidacja formatu nazwy (Domain spec)
        if (!_nameFormatSpec.IsSatisfiedBy(name))
        {
            validation.AddError(_nameFormatSpec.ErrorCode, _nameFormatSpec.ErrorMessage, "Name");
        }

        // 2. Zakazane frazy (Async spec z Application)
        var notForbiddenSpec = new ProductNameNotForbiddenSpecification(_forbiddenPhrases);
        if (!await notForbiddenSpec.IsSatisfiedByAsync(name, cancellationToken))
        {
            validation.AddError(notForbiddenSpec.ErrorCode, notForbiddenSpec.ErrorMessage, "Name");
        }

        // 3. Unikalność nazwy
        if (await _products.ExistsByNameAsync(name, cancellationToken))
        {
            validation.AddError("Product.Name.NotUnique", "Product name must be unique.", "Name");
        }

        // 4. Zakres ceny
        if (!_priceRangeSpec.IsSatisfiedBy((category, price)))
        {
            validation.AddError(_priceRangeSpec.ErrorCode, _priceRangeSpec.ErrorMessage, "Price");
        }

        // 5. Waga (jeśli wymagana)
        if (!_weightRequiredSpec.IsSatisfiedBy((type, weightKg)))
        {
            validation.AddError(_weightRequiredSpec.ErrorCode, _weightRequiredSpec.ErrorMessage, "WeightKg");
        }

        // 6. Ilość
        if (quantity < 0)
        {
            validation.AddError("Product.Quantity.Negative", "Quantity cannot be negative.", "Quantity");
        }

        if (!validation.IsValid)
        {
            return (validation, null);
        }

        #endregion Walidacja

        #region Tworzenie encji domenowej

        var product = new Product(
            Guid.NewGuid(),
            name,
            type,
            category,
            weightKg,
            price,
            quantity,
            description);

        await _products.AddAsync(product, cancellationToken);

        #endregion Tworzenie encji domenowej

        #region Historia

        var historyEntry = new ProductHistoryEntry(
            Guid.NewGuid(),
            product.Id,
            ProductChangeType.Created,
            DateTime.UtcNow,
            changedBy: null,
            oldValue: null,
            newValue: $"Created product '{name}' with price {price} and quantity {quantity}");

        await _history.AddAsync(historyEntry, cancellationToken);

        #endregion Historia

        return (validation, product);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _products.GetAllAsync(cancellationToken);

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _products.GetByIdAsync(id, cancellationToken);

    public async Task<IReadOnlyList<ProductHistoryEntry>> GetHistoryAsync(Guid productId, CancellationToken cancellationToken = default)
        => await _history.GetByProductIdAsync(productId, cancellationToken);

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return false;

        await _products.RemoveAsync(product, cancellationToken);

        var entry = new ProductHistoryEntry(
            Guid.NewGuid(),
            product.Id,
            ProductChangeType.Deleted,
            DateTime.UtcNow,
            null,
            oldValue: $"Deleted product '{product.Name}'",
            newValue: null);

        await _history.AddAsync(entry, cancellationToken);

        return true;
    }

    public async Task<(ValidationResult Validation, Product? Product)> UpdateAsync(
        Guid id,
        string name,
        EquipmentType type,
        ProductCategory category,
        decimal? weightKg,
        int quantity,
        decimal price,
        string? description,
        CancellationToken cancellationToken = default)
    {
        // TODO: napisać UpdateAsync
        // podobnie jak Create, tylko:
        // - wczytuje produkt z repo
        // - porównuje stare wartości z nowymi
        // - zapisuje zmiany i historie
        throw new NotImplementedException();
    }
}