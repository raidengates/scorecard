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
    public class GetBoardGameValidator : BaseQueryValidator<GetBoardGameQuery>
    {
        public GetBoardGameValidator()
        {
            RuleFor(x => x.GameId).NotEmpty().NotNull();
            RuleFor(x => x.Identity).NotEmpty().NotNull();
        }
    }
}
