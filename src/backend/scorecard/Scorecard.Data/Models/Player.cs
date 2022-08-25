using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Data.Models
{
    public class Player : ModelBase<Guid>
    {
        public Player() : base(Guid.NewGuid())
        {
        }
        public Player(Guid id) : base(id)
        {
        }
        public string PlayerName { get; set; }
    }
}
