using GymEquipment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.ForbiddenPhrases
{
    public class ForbiddenPhrase : Entity
    {
        public string Phrase { get; }

        private ForbiddenPhrase() { }

        public ForbiddenPhrase(Guid id, string phrase) : base(id)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                throw new DomainException("Forbidden phrase cannot be empty.");

            Phrase = phrase;
        }
    }
}
