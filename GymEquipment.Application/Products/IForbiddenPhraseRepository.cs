using GymEquipment.Domain.ForbiddenPhrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Application.Products
{
    public interface IForbiddenPhraseRepository
    {
        Task<IReadOnlyList<ForbiddenPhrase>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
