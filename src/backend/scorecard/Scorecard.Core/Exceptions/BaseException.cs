using Scorecard.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Exceptions
{
    public class BaseException : Exception
    {
        public ExceptionType Type { get; set; }

        public BaseException(ExceptionType type)
        {
            this.Type = type;
        }

        public BaseException(ExceptionType type, string message) : base(message)
        {
            this.Type = type;
        }

        public BaseException(ExceptionType type, string message, Exception innerException) : base(message, innerException)
        {
            this.Type = type;
        }
    }
}
