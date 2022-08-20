using Scorecard.Core.DependencyInjection;

namespace Scorecard.QueryHandler
{
    public class HandlerBootstrapper : IWireUpDependencies
    {
        private readonly IRegisterDependencies container;

        public HandlerBootstrapper(IRegisterDependencies container)
        {
            this.container = container;
        }
        public virtual void WireUp()
        {
            //container.Register(typeof(SaveContactHandler));
            container.Register(typeof(ServiceLoginQueryHandler));
        }

        public List<Type> GetEventTypes()
        {
            return new List<Type>();
        }
    }

}
