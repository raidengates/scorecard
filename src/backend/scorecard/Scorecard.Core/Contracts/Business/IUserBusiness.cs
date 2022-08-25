using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Queries;
using Scorecard.Applicatioin.Results;

namespace Scorecard.Core.Contracts.Business
{
    public interface IUserBusiness
    {
        Task Save(SignupCommand command);
        Task<ServiceLoginResult> Login(ServiceLoginQuery query);
    }
}
