using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Initial.Api.Initializers
{
    public static class LogInit
    {
        public static void Setup()
        {
            var appSettings = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            var connectionString = appSettings
                .GetConnectionString("InitialDatabase");

            var tableName = "Log";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionString, tableName)
                .CreateLogger();
        }
    }
}
