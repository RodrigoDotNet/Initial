using System;
using Initial.Api.Initializers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Initial.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .Build();

            CreateDbIfNotExists(host);

            LogInit.Setup();

            try
            {
                Log.Information("Starting web host");

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Configurei o banco de dados para utilizar um SQLEXPRESS.
        /// Se precisar mudar isso, veja a string de conexão no 'appsettings.json'.
        /// </summary>
        private static void CreateDbIfNotExists(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services
                        .GetRequiredService<Models.Database.InitialDatabase>();

                    context.Seed();
                }
                catch (Exception ex)
                {
                    var logger = services
                        .GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
