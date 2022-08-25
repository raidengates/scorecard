using Newtonsoft.Json;
using Scorecard.Web.Api.Extensions;
using System.Text;

namespace Scorecard.Web.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private static long _concurencyUser = 0;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestStartTime = DateTime.Now;
            var requestInfo = new StringBuilder();
            var shouldReadRequestBody = context.Request.ContentLength < 20_000_000;
            var requestIP = string.Empty;
            bool hasError = false;
            _concurencyUser++;
            try
            {
                try
                {
                    if (shouldReadRequestBody)
                    {
                        if (context.Request.Query?.Any() ?? false)
                        {
                            var query = context.Request.Query.Select(p => $"{p.Key}: {p.Value}").ToList();
                            requestInfo.Append($" Query {JsonConvert.SerializeObject(query)}");
                        }

                        if (context.Request.ContentType?.Contains("json") ?? false)
                        {
                            context.Request.EnableBuffering();
                            if (context.Request.Body.CanSeek)
                            {
                                var bufferSize = context.Request.ContentLength > 5_000 ? 5_000 : (int)context.Request.ContentLength;
                                using (var reader = new StreamReader(
                                        context.Request.Body,
                                        encoding: Encoding.UTF8,
                                        detectEncodingFromByteOrderMarks: false,
                                        bufferSize: 1024,
                                        leaveOpen: true))
                                {
                                    context.Request.Body.Position = 0;
                                    var buffer = new char[bufferSize];
                                    await reader.ReadBlockAsync(buffer, 0, bufferSize);
                                    context.Request.Body.Position = 0;
                                    var content = new string(buffer);
                                    requestInfo.Append(content);
                                }
                            }
                        }
                    }
                    requestIP = context.GetRemoteIPAddress()?.ToString();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                }
                await _next(context);
            }
            catch
            {
                hasError = true;
                throw;
            }
            finally
            {
                var user = context.User?.FindFirst("name") ?? context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn");
                var requestEndTime = DateTime.Now;
                var time = (requestEndTime - requestStartTime).TotalMilliseconds;
                _logger.LogInformation("[RequestLog]: IP: {requestIP} User: {user}, method: {method}, path: {path}, status: {stauts}.{msg}, requestInfo {requestInfo}, start at {requestStartTime}, end at {requestEndTime}, time {time} ms, concurency user {_concurencyUser}",
                        requestIP,
                        user?.Value,
                        context.Request?.Method,
                        context.Request?.Path,
                        hasError ? 500 : 200,
                        hasError ? " See error details above." : string.Empty,
                        requestInfo,
                        requestStartTime,
                        requestEndTime,
                        time,
                        _concurencyUser
                    );
                _concurencyUser--;
            }
        }
    }
}
