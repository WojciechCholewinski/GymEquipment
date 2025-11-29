using GymEquipment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GymEquipment.Domain.Products.Specifications
{
    public class ProductNameFormatSpecification : ISpecification<string>
    {
        private static readonly Regex NameRegex = new("^[a-zA-Z0-9]{3,20}$");

        public string ErrorCode => "Product.Name.InvalidFormat";

        public string ErrorMessage => "Name must be 3-20 characters long and contain only letters and digits.";

        public bool IsSatisfiedBy(string candidate) 
            => !string.IsNullOrWhiteSpace(candidate) && NameRegex.IsMatch(candidate);


    }
}
