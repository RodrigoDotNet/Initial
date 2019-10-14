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

            services.AddScoped<AccountTicketBinderFilter>();

            AccountScope.Bind(services);

            EnterpriseScope.Bind(services);
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