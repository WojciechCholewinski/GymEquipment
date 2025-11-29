using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymEquipment.Application.Common
{
    public class ValidationError
    {
        public string Code { get; }
        public string Message { get; }
        public string? Field { get; }

        public ValidationError(string code, string message, string? field = null)
        {
            Code = code;
            Message = message;
            Field = field;
        }
    }

    public class ValidationResult
    {
        private readonly List<ValidationError> _errors = [];

        public bool IsValid => _errors.Count == 0;
        public IReadOnlyCollection<ValidationError> Errors => _errors.AsReadOnly();

        public void AddError(string code, string message, string? field = null)
            => _errors.Add(new ValidationError(code, message, field));
    }
}
