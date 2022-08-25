using FluentValidation.Results;
using Scorecard.Core.Enum;

namespace Scorecard.Core.Exceptions
{
    public class CustomResponseException : BaseException
    {
        public CustomResponseException(string message) : base(ExceptionType.CustomResponseException, message)
        {
        }

        public CustomResponseException(string message, ValidationFailure error) : base(ExceptionType.Duplicated, message)
        {
        }

        public CustomResponseException(string message, Exception innerException) : base(ExceptionType.Duplicated, message, innerException)
        {
        }
    }

}
