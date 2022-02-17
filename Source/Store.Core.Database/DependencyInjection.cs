using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;
using Store.Core.Database.Repositories.RecordRepository;
using Store.Core.Database.Repositories.RoleRepository;
using Store.Core.Database.Repositories.SellerRepository;
using Store.Core.Database.Repositories.UserRepository;

namespace Store.Core.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStoreMongo(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(DbConfig));
            
            services.Configure<DbConfig>(option => configSection.Bind(option));
            services.AddSingleton<IDbContext, DbContext>();
            
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}