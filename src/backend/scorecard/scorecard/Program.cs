using Autofac;
using Autofac.Extensions.DependencyInjection;
using Scorecard.Core.DependencyInjection;
using Scorecard.Data;
using Scorecard.Data.Context;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
     .ConfigureContainer<ContainerBuilder>((builder) =>
     {
         builder.RegisterType<CoreContainerAdapter>().As<IRegisterDependencies>();
         builder.RegisterType<CoreContainerAdapter>().As<IResolveDependencies>();
         var container = new CoreContainerAdapter(builder);
         container.RegisterPerLifetime<IMongoContext, MongoDbContext>();
         container.RegisterPerLifetime<IUnitOfWork, UnitOfWork>();
         container.RegisterPerLifetime<IUserRepository, UserRepository>();
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

app.Run();
