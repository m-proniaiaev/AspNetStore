using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Services.Authorization.BlackList;
using Store.Core.Services.Authorization.PasswordProcessor;
using Store.Core.Services.Authorization.Roles;
using Store.Core.Services.Authorization.Users;
using Store.Core.Services.Common.Interfaces;
using Store.Core.Services.Internal.Records;
using Store.Core.Services.Internal.Sellers;

namespace Store.Core.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            var currentDomain = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly =>
                {
                    var name = assembly.GetName().Name;
                    return name != null && name.StartsWith("Store");
                }).ToArray();

            services.AddMediatR(currentDomain);
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlackListService, BlackListService>();
            services.AddSingleton<HashingOptions>();
            services.AddTransient<IHasher, Hasher>();

            return services;
        }
    }
}