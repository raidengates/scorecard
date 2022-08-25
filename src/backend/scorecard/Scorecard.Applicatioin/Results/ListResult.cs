using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Applicatioin.Results
{
    public class ListResult<T>
    {
        public List<T> Items { get; set; }
        public long TotalRecords { get; set; }
    }
}
