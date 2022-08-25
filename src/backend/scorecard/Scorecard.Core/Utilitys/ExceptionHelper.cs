using FluentValidation.Results;
using Scorecard.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Utilitys
{

    public class ExceptionHelper
    {
        public static Exception GetInvalidValidationException(string propertyName, string message)
        {
            return new InvalidValidationException(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
            });
        }

        public static void ThrowInvalidValidationException(string propertyName, string message)
        {
            throw new InvalidValidationException(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
            });
        }

        public static void ThrowInvalidValidationException(string propertyName, string message, string errorCode)
        {
            throw new InvalidValidationException(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
                {
                    ErrorCode = errorCode
                }
            });
        }

        public static void ThrowInvalidValidationException(string eventType, object errorResourse)
        {
            throw new NotImplementedException();
        }
        public static void ThrowAuthenticationException(string propertyName, string message)
        {
            var Failure = new ValidationFailure(propertyName, message);
            //Failure.ErrorCode = $"{HttpStatusCode.Unauthorized.ToString()} {(int)HttpStatusCode.Unauthorized}";
            Failure.ErrorCode = HttpStatusCode.Unauthorized.ToString();
            throw new AuthenticationException(new List<ValidationFailure>
            {
                Failure
            });
        }


    }

}
