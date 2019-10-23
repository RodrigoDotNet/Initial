using Initial.Api.Filters;
using Initial.Api.Models;
using Initial.Api.Models.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Initial.Api.Initializers
{
    public static class ServiceInit
    {
        public static void ConfigureService
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InitialDatabase>(options =>
                options.UseSqlServer(configuration.GetConnectionString("InitialDatabase"))
            );

            // A chamada a este Filter aqui, permite que ele também utilize o AccountScope

            services.AddScoped<AccountTicketBinderFilter>();

            // Inicializadores dos serviços e repositórios da aplicação

#warning Separar em módulos

            EnterpriseScope.Bind(services);

            AccountScope.Bind(services);

            GroupScope.Bind(services);

            AreaScope.Bind(services);

            CustomerScope.Bind(services);
        }

        public static void ConfigureErrorHandler
            (this IApplicationBuilder app, IHostingEnvironment env)
        {
            // Ref: @ErrorHandler

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
        }
    }
}