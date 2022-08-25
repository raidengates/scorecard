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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [Authorize(Role.User, Role.Admin)]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand request)
        {
            request.Identity = Identity.Identity;
            await _dispatcher.SendAsync(request);
            return Ok();
        }

        [HttpPost]
        [Route("get-board-game")]
        [ProducesResponseType(typeof(ListResult<GameResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [Authorize(Role.User, Role.Admin)]
        public async Task<IActionResult> Get([FromBody] GetBoardGameQuery request)
        {
            //GetBoardGameQuery
            request.Identity = Identity.Identity;
            var result = await _dispatcher.GetResultAsync(request);
            return Ok(result);
        }

    }
}
