using GymEquipment.Application.Products;
using GymEquipment.Domain.ForbiddenPhrases;
using GymEquipment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymEquipment.Infrastructure.Repositories
{
    public class ForbiddenPhraseRepository(GymEquipmentDbContext db) : IForbiddenPhraseRepository
    {
        private readonly GymEquipmentDbContext _db = db;

        public async Task<IReadOnlyList<ForbiddenPhrase>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _db.ForbiddenPhrases
                .OrderBy(x => x.Phrase)
                .ToListAsync(cancellationToken);
    }
}
