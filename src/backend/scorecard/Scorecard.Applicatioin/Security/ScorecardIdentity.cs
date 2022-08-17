using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Applicatioin.Security
{
    public sealed class ScorecardIdentity
    {
        public Guid Identity { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string[] Permissions { get; set; }
    }
}
