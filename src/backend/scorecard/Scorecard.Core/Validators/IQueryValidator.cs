using Scorecard.Applicatioin.Queries;
using Scorecard.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Validators
{
    public interface IQueryValidator<T> where T : BaseQuery
    {
        void ValidateQuery(T query);
        IList<FieldError> GetQueryErrors(T query);
    }
}
