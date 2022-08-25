using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Scorecard.Core.Exceptions
{
    public class InvalidValidationException : Exception
    {
        public readonly IList<FieldError> Errors;

        public InvalidValidationException(IList<FluentValidation.Results.ValidationFailure> errors)
        {
            Errors = errors.Select((FluentValidation.Results.ValidationFailure item) => new FieldError
            {
                PropertyName = item.PropertyName,
                ErrorCode = item.ErrorCode,
                ErrorMessage = item.ErrorMessage
            }).ToList();
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(Errors);
        }
    }

}
