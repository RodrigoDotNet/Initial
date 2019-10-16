using Microsoft.Extensions.DependencyInjection;

namespace Initial.Api.Models
{
    public static class GroupScope
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IGroupRepository, GroupRepository>();
        }
    }
}
