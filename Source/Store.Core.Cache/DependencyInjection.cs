using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Core.Cache.Redis;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Cache
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStoreCache(this IServiceCollection services,
            IConfiguration configuration)
        {
            var redisCacheOptions = new CacheOptions(); 
            configuration.GetSection(nameof(CacheOptions)).Bind(redisCacheOptions);
            
            services.Configure<CacheOptions>(option => configuration.GetSection(nameof(CacheOptions)).Bind(option));
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { redisCacheOptions.Server },
                    SyncTimeout = 15000
                }; 
            });
            
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}