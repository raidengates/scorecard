namespace Scorecard.Data.Models
{
    public class PointOfRound : ModelBase<Guid>
    {
        public PointOfRound() : base(Guid.NewGuid())
        {

        }
        public PointOfRound(Guid id) : base(id)
        {
        }
        public string RoundName { get; set; }
        public List<PointOfPlayer> Point { get; set; }
    }
}
