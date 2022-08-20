using Kledex.Extensions;
using Kledex.Store.EF.InMemory.Extensions;
using Scorecard.QueryHandler;
using Scorecard.Web.Api.Extensions;

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
            services.AddSwaggerDocumentation();
            //services.LoadFromApi();
            services.LoadFromServerEx();
            services.AddKledex(typeof(HandlerBootstrapper)).AddInMemoryStore();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerDocumentation();
            app.UseRouting();
            // --------------------- Custom UI ----------------
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseKledex();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
