using GymEquipment.Application.Products;
using GymEquipment.Domain.History;
using GymEquipment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymEquipment.Infrastructure.Repositories
{
    public class ProductHistoryRepository(GymEquipmentDbContext db) : IProductHistoryRepository
    {
        private readonly GymEquipmentDbContext _db = db;

        public async Task AddAsync(ProductHistoryEntry entry, CancellationToken cancellationToken = default)
        {
            await _db.ProductHistoryEntries.AddAsync(entry, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ProductHistoryEntry>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _db.ProductHistoryEntries
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.ChangedAtUtc)
                .ToListAsync(cancellationToken);
    }
}
