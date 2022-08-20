using Scorecard.Core.DependencyInjection;

namespace Scorecard.CommandHandler
{
    public class CommandHandlerBootstrapper : IWireUpDependencies
    {
        private readonly IRegisterDependencies container;

        public CommandHandlerBootstrapper(IRegisterDependencies container)
        {
            this.container = container;
        }
        public virtual void WireUp()
        {
            container.Register(typeof(AccountCommandHandler));
        }

        public List<Type> GetEventTypes()
        {
            return new List<Type>();
        }
    }
}
