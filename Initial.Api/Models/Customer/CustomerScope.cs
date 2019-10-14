using Microsoft.Extensions.DependencyInjection;

namespace Initial.Api.Models
{
    public static class CustomerScope
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
