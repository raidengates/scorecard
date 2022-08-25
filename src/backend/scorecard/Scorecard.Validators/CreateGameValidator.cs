using FluentValidation;
using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Command.Enum;
using Scorecard.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Validators
{
    public class CreateGameValidator : BaseCommandValidator<CreateGameCommand>
    {
        public CreateGameValidator()
        {
            CustomRules();
        }
        protected override void CustomRules()
        {
            base.CustomRules();
            RuleFor(c => c.Identity).NotNull();
            RuleFor(c => c.Mode).NotNull();

            RuleFor(c => c.NumberOfPlayers)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Number of characters for numberOfPlayers must be greater than or equal to 2")
                .LessThanOrEqualTo(4)
                .WithMessage("Number of characters for numberOfPlayers must be less than or equal to 4");

            RuleFor(c => c.Players).NotNull();
            RuleFor(c => c)
                .Must(c => c.Players.Count() == c.NumberOfPlayers)
                .WithMessage("Quantity <players> and <numberOfPlayers> must be the same")
                .Must(c => {
                    var _mode = c.Mode;
                    if (_mode == GameMode.Unlimited)
                        return true;
                    else
                    {
                        return c.NumberOfScores > 0;
                    }
                });
        }
    }
}
