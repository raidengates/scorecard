using Scorecard.Applicatioin.Command;
using Scorecard.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Validators
{
    public interface ICommandValidator<T> where T : BaseCommand
    {
        void ValidateCommand(T command);

        IList<FieldError> GetCommandErrors(T command);
    }
}
