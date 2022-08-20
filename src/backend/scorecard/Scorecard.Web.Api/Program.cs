using Autofac;
using Autofac.Extensions.DependencyInjection;
using Scorecard.Applicatioin.Security;
using Scorecard.Business;
using Scorecard.Core.DependencyInjection;
using Scorecard.Data;
using Scorecard.Data.Context;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Repository;
using Scorecard.MemoryCache;
using Scorecard.MemoryCache.Contracts;
using Scorecard.MemoryCache.UserCache;

namespace Scorecard.Web.Api;
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                // Declare your services with proper lifetime
                builder.RegisterType<CoreContainerAdapter>().As<IRegisterDependencies>();
                builder.RegisterType<CoreContainerAdapter>().As<IResolveDependencies>();
                var container = new CoreContainerAdapter(builder);
                //container.RegisterPerLifetime<IServiceProvider, ReverseProxyServiceProvider>();
                container.RegisterPerLifetime<IMongoContext, MongoDbContext>();
                container.RegisterPerLifetime<IUnitOfWork, UnitOfWork>();
                container.RegisterPerLifetime<IMongoContext, MongoDbContext>();
                container.RegisterPerLifetime<IUnitOfWork, UnitOfWork>();
                container.RegisterPerLifetime<IUserRepository, UserRepository>();
                container.RegisterPerLifetime<IUserRepository, UserRepository>();

                //Cache
                container.RegisterPerLifetime<IUserStore<ScorecardIdentity>, UserStore<ScorecardIdentity>>();
                container.RegisterPerLifetime<ICache, Cache>();
                //container.RegisterInstance<IOptionsMonitor<DefaultServerConfig>>();
                new Scorecard.Validators.AdValidatorBootstrapper(container).WireUp();
                new AdBusinessBootstrapper(container).WireUp();
                DependencyResolver.SetResolver(container);
                builder.RegisterBuildCallback(autofacContainer =>
                {
                    container.SetContainerInstance(autofacContainer as IContainer);
                });
            })
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("config/appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"config/appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                });
            })
            .ConfigureLogging((HostBuilderContext context, ILoggingBuilder logging) =>
            {
                var enableFileLog = (bool)context.Configuration.GetSection("EnableFileLog").Get(typeof(bool));
                if (enableFileLog)
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddConsole();
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
