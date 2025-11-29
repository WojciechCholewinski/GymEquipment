using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Domain.Common
{
    public class DomainException(string message) : Exception(message)
    {
    }
}
