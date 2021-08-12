using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Services.Authorization.PasswordProcessor;
using Store.Core.Services.Authorization.Roles;
using Store.Core.Services.Authorization.Users;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Authorization
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthHostServices(this IServiceCollection services)
        {
            var currentDomain = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly =>
                {
                    var name = assembly.GetName().Name;
                    return name != null && name.StartsWith("Store");
                }).ToArray();

            services.AddMediatR(currentDomain);
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IHasher, Hasher>();

            return services;
        }
    }
}