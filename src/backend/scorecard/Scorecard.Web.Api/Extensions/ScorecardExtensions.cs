using Scorecard.Data;
using Scorecard.Data.Context;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Persistence;

namespace Scorecard.Web.Api.Extensions
{
    public static class ScorecardExtensions
    {
        public static IServiceCollection LoadFromServerEx(this IServiceCollection services)
        {
            MongoDbPersistence.Configure();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMongoContext, MongoDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
