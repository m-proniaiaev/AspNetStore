using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Store.Extensions.Logging;

namespace Store.Host
{
    public static class HostStarter
    {
         public static int Start<T>(string[] args, string serviceLogPrefix)
            where T : class
        {
            var initialConfigBuilder = new ConfigurationBuilder().AddEnvironmentVariables("ASPNETCORE_");
            
            if (args != null)
                initialConfigBuilder.AddCommandLine(args);
            
            var initialConfig = (IConfiguration)initialConfigBuilder.Build();
            
            var environment = string.IsNullOrEmpty(initialConfig[HostDefaults.EnvironmentKey])
                ? Environment.GetEnvironmentVariable("Hosting:Environment") ??
                  Environment.GetEnvironmentVariable("ASPNET_ENV")
                : initialConfig[HostDefaults.EnvironmentKey];
            
            environment ??= Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables()
                .Build();
            
            Log.Logger = LoggerInit.InitializeSeriLog(config, environment, serviceLogPrefix);
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                Log.Information("Starting service...");
                
                var webHost = Builder.CreateHostBuilder<T>(args, Log.Logger).Build();
                webHost.Run();
                
                Log.Information("Service stopped.");
                Log.CloseAndFlush();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Exception occurred while starting service.");
                return -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Logger.Error(e.ExceptionObject as Exception, $"Current domain: unhandled exception occurred. IsTerminating={e.IsTerminating}");
            if (e.IsTerminating)
            {
                Log.CloseAndFlush();
            }
        }
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Log.Logger.Error(e.Exception, "Unobserved exception occurred.");
            e.SetObserved();
        }
    }
}