using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Queries;
using Scorecard.Core.DependencyInjection;
using Scorecard.Core.Validators;

namespace Scorecard.Validators
{
    public class AdValidatorBootstrapper : BaseWireUpDependencies
    {
        public AdValidatorBootstrapper(IRegisterDependencies registerDependencies) : base(registerDependencies)
        {
        }
        public override void WireUp()
        {
            registerDependencies.Register<ICommandValidator<SignupCommand>, SignupValidator>(
                  new DependencyParameter("SignupCommand", "SignupValidator"));
            registerDependencies.Register<ICommandValidator<CreateGameCommand>, CreateGameValidator>(
             new DependencyParameter("CreateGameCommand", "CreateGameValidator"));

            registerDependencies.Register<IQueryValidator<ServiceLoginQuery>, ServiceLoginValidator>(
                 new DependencyParameter("ServiceLoginQuery", "ServiceLoginValidator"));
            registerDependencies.Register<IQueryValidator<GetBoardGameQuery>, GetBoardGameValidator>(
              new DependencyParameter("GetBoardGameQuery", "GetBoardGameValidator"));
            
        }
    }
}
