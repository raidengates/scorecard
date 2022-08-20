using Kledex.Commands;
using Kledex.Events;
using Scorecard.Applicatioin.Command;
using Scorecard.Core.Contracts.Business;
using Scorecard.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.CommandHandler
{
    public class AccountCommandHandler : ICommandHandlerAsync<SignupCommand>
    {
        private IUserBusiness _userBusiness;
        public AccountCommandHandler(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        public async Task<CommandResponse> HandleAsync(SignupCommand command)
        {
            await _userBusiness.Save(command);
            return await Task.FromResult(
                  new CommandResponse
                  {
                      Result = Enumerable.Empty<FieldError>().ToList(),
                      Events = Enumerable.Empty<IEvent>().ToList()
                  }
              );
        }
    }
}
