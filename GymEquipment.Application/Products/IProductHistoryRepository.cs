using GymEquipment.Domain.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Application.Products
{
    public interface IProductHistoryRepository
    {
        Task AddAsync(ProductHistoryEntry entry, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductHistoryEntry>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
