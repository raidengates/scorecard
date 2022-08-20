using Microsoft.AspNetCore.Mvc;
using Scorecard.Applicatioin.Security;
using Scorecard.Data.Models;

namespace Scorecard.Controllers
{
    public class BaseController : Controller
    {
        public ScorecardIdentity Identity => (ScorecardIdentity)HttpContext.Items["AuthenticationCookie"];
    }
}
