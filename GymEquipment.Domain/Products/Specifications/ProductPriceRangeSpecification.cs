using GymEquipment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.Products.Specifications
{
    public class ProductPriceRangeSpecification : ISpecification<(ProductCategory Category, decimal Price)>
    {
        public string ErrorCode => "Product.Price.OutOfRange";
        public string ErrorMessage => "Price is out of allowed range for the given category.";

        public bool IsSatisfiedBy((ProductCategory Category, decimal Price) candidate)
        {
            var (category, price) = candidate;

            if (price <= 0) return false;

            return category switch
            {
                ProductCategory.StrengthEquipment => price is >= 50m and <= 50_000m,
                ProductCategory.CardioEquipment => price is >= 100m and <= 60_000m,
                ProductCategory.Accessories => price is >= 10m and <= 5_000m,
                _ => false
            };
        }
    }
}
