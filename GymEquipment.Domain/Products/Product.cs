using GymEquipment.Domain.Common;

namespace GymEquipment.Domain.Products
{
    public class Product : Entity
    {
        public string Name { get; }
        public EquipmentType Type { get; }
        public ProductCategory Category { get; }
        public decimal? WeightKg { get; }
        public decimal Price { get; private set; }
        public int QuantityAvailable { get; private set; }
        public string? Description { get; private set; }

        private Product() { }
        public Product(
            Guid id,
            string name,
            EquipmentType type,
            ProductCategory category,
            decimal? weightKg,
            decimal price,
            int quantityAvailable,
            string? description
            ) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name is required.");

            if (quantityAvailable < 0)
                throw new DomainException("Quantity available cannot be negative.");

            if (RequiresWeight(type) && weightKg is null)
                throw new DomainException("This equipment type requires weight.");

            Id = id;
            Name = name;
            Type = type;
            Category = category;
            WeightKg = weightKg;
            ChangePrice(price);
            ChangeQuantity(quantityAvailable);
        }
        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomainException("Price must be greater than zero.");

            Price = newPrice;
        }
        public void ChangeQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new DomainException("Quantity cannot be negative.");

            QuantityAvailable = newQuantity;
        }
        public void ChangeDescription(string? description)
        {
            Description = description;
        }
        private static bool RequiresWeight(EquipmentType type)
            => type == EquipmentType.Barbell || type == EquipmentType.Dumbbell || type == EquipmentType.Plate;
    }
}
