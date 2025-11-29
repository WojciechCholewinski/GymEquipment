using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.History
{
    public enum ProductChangeType
    {
        Created,
        Updated,
        Deleted,
        PriceChanged,
        QuantityChanged,
        DescriptionChanged
    }
}
