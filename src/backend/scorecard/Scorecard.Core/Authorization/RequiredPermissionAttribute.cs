using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.Authorization
{
    public class RequiredPermissionAttribute : AuthorizeAttribute
    {
        public RequiredPermissionAttribute(params string[] permissions)
        {
            base.Policy = $"{string.Join(",", permissions)}";
        }
    }

}
