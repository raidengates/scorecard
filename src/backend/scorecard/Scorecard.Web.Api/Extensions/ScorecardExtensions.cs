using Scorecard.Data;
using Scorecard.Data.Context;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Models;
using Scorecard.Data.Persistence;
using Scorecard.MemoryCache;
using Scorecard.MemoryCache.Contracts;
using Scorecard.MemoryCache.UserCache;

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
        public static IServiceCollection LoadFromApi(this IServiceCollection services)
        {
            services.AddSingleton<IUserStore<User>, UserStore<User>>();
            services.AddSingleton<ICache, Cache>();
            return services;
        }
    }
}
