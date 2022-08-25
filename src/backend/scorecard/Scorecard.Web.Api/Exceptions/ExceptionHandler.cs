using Microsoft.AspNetCore.Diagnostics;
using Scorecard.Core.Exceptions;
using System.Net;

namespace Scorecard.Web.Api.Exceptions
{
    public static class ExceptionHandler
    {
        public static void ExceptionConfiguration(this IApplicationBuilder builder, ILogger logger)
        {
            builder.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                    {
                        context.Response.ContentType = "application/json";
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature.Error is InvalidValidationException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            logger.LogError(contextFeature.Error, "InvalidValidation");
                            await context.Response.WriteAsync(contextFeature.Error.ToString());
                        }
                        else if (contextFeature.Error is NullReferenceException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            logger.LogError(contextFeature.Error, "NullReference");
                            await context.Response.WriteAsync(contextFeature.Error.ToString());
                        }
                        else if (contextFeature.Error is CustomResponseException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            logger.LogInformation(contextFeature.Error, "ResponseCustom");
                            await context.Response.WriteAsync(contextFeature.Error.ToString());
                        }
                        else if (contextFeature.Error is AuthenticationException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            logger.LogError(contextFeature.Error, "NullReference");
                            await context.Response.WriteAsync(contextFeature.Error.ToString());
                        }
                        else
                        {
                            var guidId = Guid.NewGuid().ToString();
                            logger.LogError(contextFeature.Error, $"{guidId}");
                            await context.Response.WriteAsync($"{{\"Error\": \"System encounted errors, please contact administrator with code: {guidId}\"}}");
                        }
                    }
                });
            });
        }
    }
}
