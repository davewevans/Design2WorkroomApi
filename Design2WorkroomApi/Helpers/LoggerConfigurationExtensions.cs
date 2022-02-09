using Microsoft.ApplicationInsights.Extensibility;
using Serilog.Events;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Design2WorkroomApi.Helpers
{
    // ref: https://blog.johnnyreilly.com/2021/01/30/aspnet-serilog-and-application-insights/

    internal static class LoggerConfigurationExtensions
    {
        internal static void SetupLoggerConfiguration(string appName)
        {
            Log.Logger = new LoggerConfiguration()
                .ConfigureBaseLogging(appName)
                .CreateLogger();
        }

        internal static LoggerConfiguration ConfigureBaseLogging(
            this LoggerConfiguration loggerConfiguration,
            string appName
        )
        {
            loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                // AMAZING COLOURS IN THE CONSOLE!!!!
                .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProperty("ApplicationName", appName);

            return loggerConfiguration;
        }

        internal static LoggerConfiguration AddApplicationInsightsLogging(this LoggerConfiguration loggerConfiguration, IServiceProvider services, IConfiguration configuration)
        {
            if (!string.IsNullOrWhiteSpace(configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY")))
            {
                loggerConfiguration.WriteTo.ApplicationInsights(
                    services.GetRequiredService<TelemetryConfiguration>(),
                    TelemetryConverter.Traces);
            }

            return loggerConfiguration;
        }
    }
}
