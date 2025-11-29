using GymEquipment.Application.Products;
using GymEquipment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Application.Specification
{
    public class ProductNameNotForbiddenSpecification : IAsyncSpecification<string>
    {
        private readonly IForbiddenPhraseRepository _forbiddenPhrases;

        public ProductNameNotForbiddenSpecification(IForbiddenPhraseRepository forbiddenPhrases)
        {
            _forbiddenPhrases = forbiddenPhrases;
        }

        public string ErrorCode => "Product.Name.ContainsForbiddenPhrase";
        public string ErrorMessage => "Product name contains a forbidden phrase.";

        public async Task<bool> IsSatisfiedByAsync(string candidate, CancellationToken cancellationToken = default)
        {
            var phrases = await _forbiddenPhrases.GetAllAsync(cancellationToken);

            foreach (var phrase in phrases)
            {
                if (candidate.Contains(phrase.Phrase, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }
    }
}
