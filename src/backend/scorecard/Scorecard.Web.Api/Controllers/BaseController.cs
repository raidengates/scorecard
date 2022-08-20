using Microsoft.AspNetCore.Mvc;
using Scorecard.Applicatioin.Security;

namespace Scorecard.Controllers
{
    public class BaseController : Controller
    {
        public ScorecardIdentity Identity { get; set; }
    }
}
