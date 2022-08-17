using Autofac;
using Autofac.Extensions.DependencyInjection;
using Scorecard.Business;
using Scorecard.Core.DependencyInjection;
using Scorecard.Data;
using Scorecard.Data.Context;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Repository;
using Kledex.Extensions;
using Kledex.Store.EF.InMemory.Extensions;
using Scorecard.QueryHandler;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
      .ConfigureContainer((Action<ContainerBuilder>)(builder =>
      {
          ConfigContainer(builder);
      }))
     .ConfigureContainer<ContainerBuilder>((builder) =>
     {
         builder.RegisterType<CoreContainerAdapter>().As<IRegisterDependencies>();
         builder.RegisterType<CoreContainerAdapter>().As<IResolveDependencies>();
         var container = new CoreContainerAdapter(builder);
         container.RegisterPerLifetime<IMongoContext, MongoDbContext>();
         container.RegisterPerLifetime<IUnitOfWork, UnitOfWork>();
         container.RegisterPerLifetime<IUserRepository, UserRepository>();
         
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

    });


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddKledex(typeof(HandlerBootstrapper)).AddInMemoryStore();

//builder.Services.AddJwtAuthentication(Configuration).AddBasicAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseKledex();
app.Run();


static void ConfigContainer(ContainerBuilder autofacBuilder)
{
    var builder = new CoreContainerAdapter(autofacBuilder);

    builder.RegisterInstance<IRegisterDependencies>(builder);
    builder.RegisterPerLifetime<IResolveDependencies, OneMobileResolveDependencies>();
    builder.RegisterPerLifetime<ILifetimeResolveDependencies, LifetimeResolveDependencies>();

    autofacBuilder.RegisterBuildCallback(autofacContainer =>
    {
        builder.SetContainerInstance(autofacContainer as IContainer);
    });
}