using FluentValidation;
using FluentValidation.Results;
using Scorecard.Applicatioin.Command;
using Scorecard.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Validators
{
    public abstract class BaseCommandValidator<T> : AbstractValidator<T>, ICommandValidator<T> where T : BaseCommand
    {
        protected virtual void CustomRules()
        {
        }

        public void ValidateCommand(T command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            ValidationResult validationResult = Validate(command);
            if (!validationResult.IsValid)
            {
                throw new InvalidValidationException(validationResult.Errors);
            }
        }

        public IList<FieldError> GetCommandErrors(T command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            ValidationResult validationResult = Validate(command);
            if (validationResult.IsValid)
            {
                return new List<FieldError>();
            }

            return validationResult.Errors.Select((ValidationFailure item) => new FieldError
            {
                PropertyName = item.PropertyName,
                ErrorCode = item.ErrorCode,
                ErrorMessage = item.ErrorMessage
            }).ToList();
        }
    }
}
