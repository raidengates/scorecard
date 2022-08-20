using Scorecard.Core.Contracts.Business;
using Scorecard.Core.DependencyInjection;
using Scorecard.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Business
{
    public class AdBusinessBootstrapper : BaseWireUpDependencies
    {
        public AdBusinessBootstrapper(IRegisterDependencies registerDependencies) : base(registerDependencies)
        {
        }
        public override void WireUp()
        {
            registerDependencies.Register<IUserBusiness, UserBusiness>();
        }
    }
}
