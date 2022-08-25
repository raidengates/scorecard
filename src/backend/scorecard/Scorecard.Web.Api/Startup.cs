using Kledex.Extensions;
using Kledex.Store.EF.InMemory.Extensions;
using Scorecard.CommandHandler;
using Scorecard.Core.Contracts.Config;
using Scorecard.QueryHandler;
using Scorecard.Web.Api.Exceptions;
using Scorecard.Web.Api.Extensions;
using Scorecard.Web.Api.Middleware;
using ServiceStack.Configuration;

namespace Scorecard.Web.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.Configure<AppSettings>(_configuration.GetSection("Scorecard"));
            services.Configure<DefaultServerConfig>(_configuration);
            services.AddSwaggerDocumentation();
            services.LoadFromApi();
            services.LoadFromServerEx();
            services.AddKledex(typeof(QueryHandlerBootstrapper), typeof(CommandHandlerBootstrapper)).AddInMemoryStore();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerDocumentation();
            app.UseRouting();
            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseKledex();
            app.UseHttpsRedirection();
            // --------------------- Custom Exception ----------------
            app.ExceptionConfiguration(logger);
            // --------------------- Custom Middleware ----------------
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<CacheMiddleware>();
            app.UseMiddleware<JwtMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
