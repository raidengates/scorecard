using Kledex.Commands;
using Kledex.Events;
using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Results;
using Scorecard.Core.Contracts.Business;
using Scorecard.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.CommandHandler
{
    //GameResult GameBusiness
    public class GameCommandHandler : ICommandHandlerAsync<CreateGameCommand>
    {
        private IGameBusiness _userBusiness;
        private readonly ICommandValidator<CreateGameCommand> _gameCommandValidator;
        public GameCommandHandler(IGameBusiness userBusiness, ICommandValidator<CreateGameCommand> gameCommandValidator)
        {
            _userBusiness = userBusiness;
            _gameCommandValidator = gameCommandValidator;
        }

        public async Task<CommandResponse> HandleAsync(CreateGameCommand command)
        {
            _gameCommandValidator.ValidateCommand(command);
            await _userBusiness.CreateGame(command);
            return await Task.FromResult(new CommandResponse { Events = Enumerable.Empty<IEvent>(), Result = "ok" });
        }
    }
}
