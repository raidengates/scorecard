using Scorecard.Core.DependencyInjection;

namespace Scorecard.QueryHandler
{
    public class QueryHandlerBootstrapper : IWireUpDependencies
    {
        private readonly IRegisterDependencies container;

        public QueryHandlerBootstrapper(IRegisterDependencies container)
        {
            this.container = container;
        }
        public virtual void WireUp()
        {
            container.Register(typeof(ServiceLoginQueryHandler));
        }

        public List<Type> GetEventTypes()
        {
            return new List<Type>();
        }
    }

}
