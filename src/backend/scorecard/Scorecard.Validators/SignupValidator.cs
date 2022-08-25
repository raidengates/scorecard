using FluentValidation;
using Scorecard.Applicatioin.Command;
using Scorecard.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Validators
{
    public class SignupValidator : BaseCommandValidator<SignupCommand>
    {
        public SignupValidator()
        {
            CustomRules();
        }

        protected override void CustomRules()
        {
            base.CustomRules();
            RuleFor(c => c.Username).NotNull().NotEmpty();
            RuleFor(c => c.Email).NotNull().NotEmpty();
            RuleFor(c => c.Password).NotNull().NotEmpty();
        }
    }
}
