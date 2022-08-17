using FluentValidation;
using Scorecard.Applicatioin.Queries;
using Scorecard.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Validators
{
    public class ServiceLoginValidator : BaseQueryValidator<ServiceLoginQuery>
    {
        public ServiceLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }

}
