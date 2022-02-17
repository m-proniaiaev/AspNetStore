using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;
using Store.Core.Database.Repositories.SellerRepository;

namespace Store.Core.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStoreMongo(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(DbConfig));
            DbConfig config = new();
            configSection.Bind(config);
            
            services.Configure<DbConfig>(option => configSection.Bind(option));
            services.AddSingleton<IMongoClient>(new MongoClient(config.ConnectionString));
            services.AddSingleton<IDbContext, DbContext>();
            
            services.AddSingleton<ISellerRepository, SellerRepository>();
            
            return services;
        }
    }
}