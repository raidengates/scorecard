using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Contracts.Config
{
    public interface IServerConfig
    {
        string WebDomain { get; set; }

        string[] WebAllowAccessIps { get; set; }

    }
}
