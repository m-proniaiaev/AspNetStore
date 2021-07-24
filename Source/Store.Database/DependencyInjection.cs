using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Database.Database;

namespace Store.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStoreMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbClient, DbClient>();
            services.Configure<DbConfig>(option => configuration.GetSection(nameof(DbConfig)).Bind(option));
            return services;
        }
    }
}