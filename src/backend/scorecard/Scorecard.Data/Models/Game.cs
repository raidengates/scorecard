using Scorecard.Applicatioin.Command.Enum;

namespace Scorecard.Data.Models
{
    public class Game : ModelBase<Guid>
    {
        public Game() : base(Guid.NewGuid())
        {
        }
        public Game(Guid id) : base(id)
        {
        }
        public GameMode Mode { get; set; }
        public string Name { get; set; }
        public int NumberOfPlayers { get; set; }
        public List<Player> Players { get; set; }
        public int NumberOfScores { get; set; }
        public List<PointOfRound> PointOfRound { get; set; }
    }
}
