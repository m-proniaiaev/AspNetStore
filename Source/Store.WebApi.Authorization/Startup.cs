using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Core.Cache;
using Store.Core.Contracts.Domain;
using Store.Core.Database;
using Store.Core.Host.Authorization;
using Store.Core.Host.Extensions;
using Store.Core.Services;

namespace Store.WebApi.Authorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStoreMongo(Configuration);
            services.AddStoreCache(Configuration);
            services.AddCoreServices();
            services.AddStoreAuthorization(Configuration);
            services.AddConfiguredControllers();
            services.Configure<ActionsConfig>(option => Configuration.GetSection(nameof(ActionsConfig)).Bind(option));
            services.AddStoreSwagger("Authorization");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseExtensions();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store.WebApi.Authorization v1"));
            
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}