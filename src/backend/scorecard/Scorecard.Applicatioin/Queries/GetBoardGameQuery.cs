using Scorecard.Applicatioin.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Applicatioin.Queries
{
    public class GetBoardGameQuery : BaseQuery<ListResult<GameResult>>
    {
        public Guid GameId { get; set; }
    }
}
