using Scorecard.Applicatioin.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Applicatioin.Queries
{
    public partial class ServiceLoginQuery : BaseQuery<ServiceLoginResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
