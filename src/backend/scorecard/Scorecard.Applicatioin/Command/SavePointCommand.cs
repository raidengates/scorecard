using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Applicatioin.Command
{
    public class SavePointCommand : BaseCommand
    {
        public Guid GameId { get; set; }
        public List<PointOfPlayer> Point { get; set; }

    }
}
