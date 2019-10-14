using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Initial.Api.Initializers;
using Initial.Api.Util;

namespace Initial.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            var _configurationAppSettings = _configuration.GetSection("AppSettings");

            _appSettings = _configurationAppSettings.Get<AppSettings>();
        }

        private readonly IConfiguration _configuration;

        private readonly AppSettings _appSettings;

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCompression();

            services.ConfigureMvc(_appSettings);

            services.ConfigureApi();

            services.ConfigureJwt(_appSettings);

            services.ConfigureSwagger();

            services.ConfigureService(_configuration);

            services.AddSingleton(_appSettings);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApiVersionDescriptionProvider provider
            )
        {
            app.ConfigureCompression();

            app.ConfigureErrorHandler(env);

            app.ConfigureSwagger(provider);

            app.ConfigureApi();

            app.ConfigureJwt();

            app.ConfigureMvc();
        }
    }
}
