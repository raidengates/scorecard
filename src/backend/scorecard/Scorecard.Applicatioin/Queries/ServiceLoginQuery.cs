using Scorecard.Applicatioin.Results;

namespace Scorecard.Applicatioin.Queries
{
    public partial class ServiceLoginQuery : BaseQuery<ServiceLoginResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
