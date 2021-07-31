using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Common.Interfaces;
using Store.Core.Services.Records;

namespace Store.Core
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
            services.AddScoped<IRecordService, RecordService>();

            return services;
        }
    }
}