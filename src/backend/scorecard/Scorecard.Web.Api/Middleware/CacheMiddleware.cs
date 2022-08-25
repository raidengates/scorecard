using Microsoft.Extensions.Caching.Memory;

namespace Scorecard.Web.Api.Middleware
{
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        IMemoryCache _memoryCache;
        public CacheMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context)
        {
            var Headers = context.Request.Headers;
            string authHeader = Headers["Authorization"];
            await _next(context);
        }
    }
}
