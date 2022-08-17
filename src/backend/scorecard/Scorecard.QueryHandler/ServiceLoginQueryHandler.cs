using Kledex.Queries;
using Scorecard.Applicatioin.Queries;
using Scorecard.Applicatioin.Results;
using Scorecard.Core.Contracts.Business;
using Scorecard.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.QueryHandler
{
    public class ServiceLoginQueryHandler : IQueryHandlerAsync<ServiceLoginQuery, ServiceLoginResult>
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IQueryValidator<ServiceLoginQuery> _serviceLoginValidator;
        public ServiceLoginQueryHandler(IUserBusiness userBusiness, IQueryValidator<ServiceLoginQuery> serviceLoginValidator)
        {
            _userBusiness = userBusiness;
            _serviceLoginValidator = serviceLoginValidator;
        }
        public Task<ServiceLoginResult> HandleAsync(ServiceLoginQuery query)
        {
            _serviceLoginValidator.ValidateQuery(query);
            throw new NotImplementedException();
        }
    }
}
