namespace GymEquipment.Domain.Products
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }
        public EquipmentType Type { get; }
        public decimal? WeightKg { get; }
        public decimal Price { get; private set; }
        public string? Description { get; private set; }

        private Product() { }
        public Product(Guid id, string name, EquipmentType type, decimal? weightKg, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name is required.");

            if (price <= 0)
                throw new DomainException("Price must be greater than zero.");

            if (RequiresWeight(type) && weightKg is null)
                throw new DomainException("This equipment type requires weight.");

            Id = id;
            Name = name;
            Type = type;
            WeightKg = weightKg;
            Price = price;
        }
        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomainException("Price must be greater than zero.");

            Price = newPrice;
        }

        private static bool RequiresWeight(EquipmentType type)
            => type == EquipmentType.Barbell || type == EquipmentType.Dumbbell || type == EquipmentType.Plate;
    }
}
