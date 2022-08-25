using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Results;

namespace Scorecard.Core.Contracts.Business
{
    public interface IGameBusiness
    {
        Task CreateGame(CreateGameCommand command);
        Task<List<GameResult>> GetBoardGame(Guid userId, Guid gameId);
    }
}
