using Scorecard.Applicatioin.Command.Enum;

namespace Scorecard.Applicatioin.Command
{
    public class CreateGameCommand : BaseCommand
    {
        public GameMode Mode { get; set; }
        public string Name { get; set; }
        public int NumberOfPlayers { get; set; }
        public int NumberOfScores { get; set; }
        public List<Player> Players { get; set; }
        public List<PointOfRound> PointOfRound { get; set; }
    }
    public class Player
    {
        public string PlayerName { get; set; }
    }
    public class PointOfRound {
        public string RoundName { get; set; }
        public List<PointOfPlayer> Point { get; set; }
    }
    public class PointOfPlayer
    {
        public string PlayerName { get; set; }
        public long Point { get; set; }
        public long PointOfBonus { get; set; }
        public long Total { get; set; }
    }
}
