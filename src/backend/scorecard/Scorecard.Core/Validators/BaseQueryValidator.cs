using FluentValidation;
using FluentValidation.Results;
using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Queries;
using Scorecard.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Validators
{

    public abstract class BaseQueryValidator<T> : AbstractValidator<T>, IQueryValidator<T> where T : BaseQuery
    {
        public IList<FieldError> GetQueryErrors(T query)
        {
            if ((object)query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            ValidationResult result = this.Validate(query);
            if (result.IsValid)
            {
                return new List<FieldError>();
            }

            return result.Errors.Select(e => new FieldError
            {
                ErrorCode = e.ErrorCode,
                ErrorMessage = e.ErrorMessage,
                PropertyName = e.PropertyName
            }).ToList();
        }

        public void ValidateQuery(T query)
        {
            if ((object)query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            ValidationResult result = this.Validate(query);
            if (!result.IsValid)
            {
                throw new InvalidValidationException(result.Errors);
            }
        }
    }
}
