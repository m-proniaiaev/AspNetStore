using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Store.Core.Host.Extensions.Logging
{
    public class LoggerInit
    {
        public static Serilog.Core.Logger InitializeSeriLog(IConfiguration configuration, string environment, string indexPrefix)
        {
            var loggerConfiguration = new LoggerConfiguration();
        
            var settings = GetSettings(configuration);
        
            var appName = Assembly.GetEntryAssembly()?.GetName().Name;
        
            loggerConfiguration = loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProperty("Application", appName)
                .Enrich.WithProperty("Environment", environment)
                .Enrich.WithProperty("Service", indexPrefix)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning);
        
            loggerConfiguration = loggerConfiguration.MinimumLevel.Is(settings.MinimumLogLevel);
            
        
            return loggerConfiguration.CreateLogger();
        }
        
        private static LoggerSettings GetSettings(IConfiguration configuration)
        {
            var configSection = configuration.GetSection("Logger");
            var loggerSettings = new LoggerSettings();
            if (configSection.Exists()) configSection.Bind(loggerSettings);
        
            return loggerSettings;
        }
    }

   public class LoggerSettings
   {
       public LogEventLevel MinimumLogLevel { get; set; } = LogEventLevel.Debug;
   }
}