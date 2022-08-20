// Licensed under the Apache License, Version 1.0 (the "License").

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scorecard.Applicatioin.Security;
using Scorecard.Core.Utilitys;
using Scorecard.Data.Models;

namespace Scorecard.Web.Api.Helpers;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<Role> _roles;

    public AuthorizeAttribute(params Role[] roles)
    {
        _roles = roles ?? new Role[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var account = (ScorecardIdentity)context.HttpContext.Items["AuthenticationCookie"];
        if (account == null)
        {
            // not logged in or role not authorized
            ExceptionHelper.ThrowAuthenticationException("Authorization", "Unauthorized");
        }
        var currentRoles = account.Roles.ToList();
        var roles = _roles.Select(x => (int)x).ToArray();
        var rolesContracts = currentRoles.Where(x => roles.Contains(x));
        if (!(_roles.Any() && rolesContracts.Any()))
        {
            // not logged in or role not authorized
            ExceptionHelper.ThrowAuthenticationException("Authorization", "Unauthorized");
        }
    }
}
