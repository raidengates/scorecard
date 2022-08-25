using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scorecard.Applicatioin.Security;
using Scorecard.Core.Contracts.Config;
using Scorecard.Core.Utilitys;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Models;
using Scorecard.MemoryCache.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Text;

namespace Scorecard.Web.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        IUserStore<ScorecardIdentity> _userStore;
        public JwtMiddleware(RequestDelegate next, IUserStore<ScorecardIdentity> userStore)
        {
            _next = next;
            _userStore = userStore;
        }
        public async Task Invoke(HttpContext context, IUserRepository userRepository, IOptionsMonitor<DefaultServerConfig> optionsMonitor)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
                await attachAccountToContext(context, userRepository, token, optionsMonitor.CurrentValue.Api.JWT.IssuerSigningKey);
            await _next(context);
        }
        private async Task attachAccountToContext(HttpContext context, IUserRepository userRepository, string token, string IssuerSigningKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(IssuerSigningKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //attach account to context on successful jwt validation
                ScorecardIdentity userInfo = _userStore.GetUserCache(accountId);
                if (userInfo == null)
                {
                    Expression<Func<User, bool>> predicate = _user => _user.Id == accountId;
                    var user = (await userRepository.Predicates(predicate)).FirstOrDefault();
                    userInfo = new ScorecardIdentity()
                    {
                        Email = user.Email,
                        Identity = user.Id,
                        Roles = user.Permissions.Select(s => (int)s.Role).ToArray(),
                    };
                    _userStore.ReloadUserCache(accountId, userInfo);
                }
                context.Items["AuthenticationCookie"] = userInfo;
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
                ExceptionHelper.ThrowAuthenticationException("Authorization", "Unauthorized");
            }
        }
    }

}
