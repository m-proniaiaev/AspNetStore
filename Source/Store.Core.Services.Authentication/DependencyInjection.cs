using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Services.AuthHost.Common.Interfaces;
using Store.Core.Services.AuthHost.PasswordProcessor;
using Store.Core.Services.AuthHost.Services.Roles;
using Store.Core.Services.AuthHost.Services.Users;

namespace Store.Core.Services.AuthHost
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