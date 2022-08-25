using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Data.Models
{
    public class PointOfPlayer : ModelBase<Guid>
    {
        public PointOfPlayer() : base(Guid.NewGuid())
        {
        }
        public PointOfPlayer(Guid id) : base(id)
        {
        }
        public string PlayerName { get; set; }
        public long Point { get; set; }
        public long PointOfBonus { get; set; }
        public long Total { get; set; }
    }
}
