using Microsoft.Extensions.DependencyInjection;

namespace Initial.Api.Models
{
    public static class AreaScope
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddScoped<IAreaService, AreaService>();

            services.AddScoped<IAreaRepository, AreaRepository>();
        }
    }
}
