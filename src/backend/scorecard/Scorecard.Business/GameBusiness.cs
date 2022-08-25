using ReflectionMagic;
using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Results;
using Scorecard.Core.Contracts.Business;
using Scorecard.Core.Utilitys;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Models;
using Player = Scorecard.Applicatioin.Command.Player;
using PointOfRound = Scorecard.Data.Models.PointOfRound;
namespace Scorecard.Business
{
    public class GameBusiness : IGameBusiness
    {
        IUnitOfWork _uow;
        IUserRepository _userRepository;

        public GameBusiness(IUserRepository userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }
        public async Task CreateGame(CreateGameCommand command)
        {
            var userInfo = await _userRepository.GetById((Guid)command.Identity);
            if (userInfo == null)
            {
                ExceptionHelper.ThrowInvalidValidationException(nameof(command.Identity), "account not exist");
            }

            Game newGame = new Game()
            {
                LastUpdatedBy = command.Identity.ToString(),
                CreatedBy = command.Identity.ToString(),
                CreatedDateTime = DateTime.Now,
                LastUpdatedDateTime = DateTime.Now,
                NumberOfPlayers = command.NumberOfPlayers,
                NumberOfScores = command.NumberOfScores,
                Mode = command.Mode,
                Name = command.Name,
                Players = command.Players.Select(x => new Data.Models.Player()
                {
                    LastUpdatedBy = command.Identity.ToString(),
                    CreatedBy = command.Identity.ToString(),
                    CreatedDateTime = DateTime.Now,
                    LastUpdatedDateTime = DateTime.Now,
                    PlayerName = x.PlayerName
                }).ToList(),
                PointOfRound = command.PointOfRound.Select(x => new Data.Models.PointOfRound()
                {
                    LastUpdatedBy = command.Identity.ToString(),
                    CreatedBy = command.Identity.ToString(),
                    CreatedDateTime = DateTime.Now,
                    LastUpdatedDateTime = DateTime.Now,
                    Point = x.Point.Select(p => new Data.Models.PointOfPlayer()
                    {
                        LastUpdatedBy = command.Identity.ToString(),
                        CreatedBy = command.Identity.ToString(),
                        CreatedDateTime = DateTime.Now,
                        LastUpdatedDateTime = DateTime.Now,
                        PlayerName = p.PlayerName,
                        Point = p.Point,
                        PointOfBonus = p.PointOfBonus,
                        Total = p.Total,
                    }).ToList(),
                    RoundName = x.RoundName
                    
                }).ToList()
            };
            if (userInfo.Games == null)
            {
                userInfo.Games = new List<Game>();
            }
            userInfo.Games.Add(newGame);
            _userRepository.Update(userInfo);
            _uow.Commit();
        }

        public async Task<List<GameResult>> GetBoardGame(Guid userId, Guid gameId)
        {
            var userInfo = await _userRepository.GetById(userId);
            if (userInfo == null)
            {
                ExceptionHelper.ThrowInvalidValidationException(nameof(userId), "account not exist");
            }
            List<GameResult> boardGame = new List<GameResult>();

            if(userInfo.Games != null)
            {
                var _game = userInfo.Games.Where(x => x.Id == gameId);
                if(_game.Any())
                {
                    boardGame = _game.Select(game => new GameResult()
                    {
                        Mode = game.Mode,
                        Name = game.Name,
                        NumberOfPlayers = game.NumberOfPlayers,
                        NumberOfScores = game.NumberOfScores,
                        Players = game.Players.Select( p => new Player()
                        {
                            PlayerName = p.PlayerName,
                        }).ToList(),
                        PointOfRound = game.PointOfRound.Select(point => new Applicatioin.Command.PointOfRound()
                        {
                           RoundName = point.RoundName,
                           Point = point.Point.Select(p => new Applicatioin.Command.PointOfPlayer() { 
                                Total = p.Total,
                                PlayerName = p.PlayerName,
                                Point = p.Point,
                                PointOfBonus = p.PointOfBonus
                           }).ToList()
                        }).ToList()
                        
                    }).ToList();
                }
            }
            return boardGame;
        }
    }
}
