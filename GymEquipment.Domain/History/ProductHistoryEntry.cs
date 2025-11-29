using GymEquipment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.History
{
    public class ProductHistoryEntry : Entity
    {
        public Guid ProductId { get; }
        public ProductChangeType ChangeType { get; }
        public DateTime ChangedAtUtc { get; }
        public string? ChangedBy { get; }
        public string? OldValue { get; }
        public string? NewValue { get; }

        private ProductHistoryEntry() { }

        public ProductHistoryEntry(
            Guid id,
            Guid productId,
            ProductChangeType changeType,
            DateTime changedAtUtc,
            string? changedBy,
            string? oldValue,
            string? newValue)
            : base(id)
        {
            ProductId = productId;
            ChangeType = changeType;
            ChangedAtUtc = changedAtUtc;
            ChangedBy = changedBy;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
