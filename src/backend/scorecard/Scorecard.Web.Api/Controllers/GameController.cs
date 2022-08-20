using Kledex;
using Microsoft.AspNetCore.Mvc;
using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Queries;
using Scorecard.Applicatioin.Results;
using Scorecard.Controllers;
using Scorecard.Core.Exceptions;
using Scorecard.Data.Models;
using Scorecard.Web.Api.Helpers;
using System.Net;
namespace Scorecard.Web.Api.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        public GameController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("create-game")]
        [ProducesResponseType(typeof(ServiceLoginResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [Authorize(Role.User, Role.Admin)]
        public async Task<IActionResult> ServiceLogin([FromBody] ServiceLoginQuery request)
        {
            ServiceLoginResult result = await _dispatcher.GetResultAsync(request);
            return Ok(result);
        }

    }
}
