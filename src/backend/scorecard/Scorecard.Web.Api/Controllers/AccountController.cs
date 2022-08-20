using Kledex;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scorecard.Applicatioin.Queries;
using Scorecard.Applicatioin.Results;
using System.Net;

namespace Scorecard.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        public AccountController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ServiceLoginResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ServiceLogin([FromBody] ServiceLoginQuery request)
        {
            ServiceLoginResult result = await _dispatcher.GetResultAsync(request);
            return Ok(new ServiceLoginResult()
            {
                acccess_token = result.acccess_token,
                id_token = result.id_token,
                expires_in = result.expires_in,
                refresh_token = result.refresh_token,
                refresh_token_expires_in = result.refresh_token_expires_in,
                token_type = result.token_type,
            });
        }


    }
}
