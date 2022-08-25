using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Exceptions
{
    public class FieldError
    {
        public string PropertyName { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }

}
