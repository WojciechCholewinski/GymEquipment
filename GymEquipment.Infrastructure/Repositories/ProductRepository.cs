using GymEquipment.Application.Products;
using GymEquipment.Domain.Products;
using GymEquipment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymEquipment.Infrastructure.Repositories
{
    public class ProductRepository(GymEquipmentDbContext db) : IProductRepository
    {
        private readonly GymEquipmentDbContext _db = db;

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _db.Products
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _db.Products.AddAsync(product, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Product product, CancellationToken cancellationToken = default)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _db.Products
                .AnyAsync(p => p.Name == name, cancellationToken);
        }
    }
}
