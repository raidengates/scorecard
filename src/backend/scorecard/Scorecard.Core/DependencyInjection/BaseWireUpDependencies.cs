using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.DependencyInjection
{
    public abstract class BaseWireUpDependencies : IWireUpDependencies
    {
        protected readonly IRegisterDependencies registerDependencies;

        protected BaseWireUpDependencies(IRegisterDependencies registerDependencies)
        {
            if (registerDependencies == null)
            {
                throw new ArgumentNullException("registerDependencies");
            }

            this.registerDependencies = registerDependencies;
        }

        public abstract void WireUp();
    }
}
