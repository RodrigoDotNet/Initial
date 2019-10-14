using Microsoft.Extensions.DependencyInjection;

namespace Initial.Api.Models
{
    public static class AccountScope
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
