using Microsoft.Extensions.DependencyInjection;

namespace Initial.Api.Models
{
    public static class EnterpriseScope
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddScoped<IEnterpriseService, EnterpriseService>();

            services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
        }
    }
}
