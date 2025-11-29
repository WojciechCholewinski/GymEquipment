using GymEquipment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.Products.Specifications
{
    public class ProductWeightRequiredSpecification : ISpecification<(EquipmentType Type, decimal? Weight)>
    {
        public string ErrorCode => "Product.Weight.Missing";
        public string ErrorMessage => "Weight is required for this type of equipment.";

        public bool IsSatisfiedBy((EquipmentType Type, decimal? Weight) candidate)
        {
            var (type, weight) = candidate;

            var requiresWeight = type is EquipmentType.Barbell
                                      or EquipmentType.Dumbbell
                                      or EquipmentType.Plate;

            if (!requiresWeight)
                return true;

            return weight is not null && weight > 0;
        }
    }
}
