using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Store.Core.Contracts.Common;
using Store.Core.Host.Extensions.Exceptions;

namespace Store.Core.Host.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddConfiguredControllers(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly =>
                {
                    var name = assembly.GetName().Name;
                    return name != null && name.StartsWith("Store");
                }).ToArray();

            services.AddControllers(opt =>
            {
                opt.Filters.Add<ValidateModelAttribute>();
                opt.Filters.Add(new ProducesResponseTypeAttribute(typeof(ExceptionModel), StatusCodes.Status202Accepted));
                opt.Filters.Add(new ProducesAttribute("application/json"));
            }).AddJsonOptions(config =>
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                config.JsonSerializerOptions.IgnoreNullValues = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(assemblies);
                fv.DisableDataAnnotationsValidation = true;
            });
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddLogging(configure =>
            {
                configure.AddDebug();
                configure.AddConsole();
            });
            
            return services;
        }
    }
}