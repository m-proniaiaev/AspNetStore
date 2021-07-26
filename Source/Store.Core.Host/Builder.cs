using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Store.Core.Host
{
    public static class Builder
    {
        public static IHostBuilder CreateHostBuilder<TStartup>(string[] args, ILogger logger)
            where TStartup : class
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel((context, options) =>
                    {
                        options.Configure(context.Configuration.GetSection("Kestrel"));
                        options.AddServerHeader = false;
                    });
                    webBuilder.UseStartup<TStartup>();
                });
        }
    }
}