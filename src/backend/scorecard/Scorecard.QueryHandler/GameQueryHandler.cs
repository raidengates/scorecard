using Kledex.Queries;
using Scorecard.Applicatioin.Queries;
using Scorecard.Applicatioin.Results;
using Scorecard.Core.Contracts.Business;
using Scorecard.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.QueryHandler
{
    public class GameQueryHandler : IQueryHandlerAsync<GetBoardGameQuery, ListResult<GameResult>>
    {
        private readonly IGameBusiness _gameBusiness;
        private readonly IQueryValidator<GetBoardGameQuery> _serviceLoginValidator;
        public GameQueryHandler(IGameBusiness gameBusiness, IQueryValidator<GetBoardGameQuery> serviceLoginValidator)
        {
            _gameBusiness = gameBusiness;
            _serviceLoginValidator = serviceLoginValidator;
        }

        public async Task<ListResult<GameResult>> HandleAsync(GetBoardGameQuery query)
        {
            _serviceLoginValidator.ValidateQuery(query);
            var result = _gameBusiness.GetBoardGame((Guid)query.Identity, query.GameId);
            throw new NotImplementedException();
        }
    }
}
