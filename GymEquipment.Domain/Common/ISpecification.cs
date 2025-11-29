using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.Common
{
    public interface ISpecification<in T>
    {
        string ErrorCode { get; }
        string ErrorMessage { get; }

        bool IsSatisfiedBy(T candidate);
    }
}
