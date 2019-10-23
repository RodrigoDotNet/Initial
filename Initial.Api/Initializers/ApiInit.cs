using Initial.Api.Filters;
using Initial.Api.Resources;
using Initial.Api.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Initial.Api.Initializers
{
    /// <summary>
    /// Inicializador da WebApi
    /// </summary>
    public static class ApiInit
    {
        public static void ConfigureCompression(this IServiceCollection services)
        {
            // Ref: @ResponseCompression

            services.AddResponseCompression();
        }

        public static void ConfigureApi(this IServiceCollection services)
        {
            // Políticas de CORS

            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            // Versionamento

            services.AddApiVersioning();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";

                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void ConfigureMvc
            (this IServiceCollection services, AppSettings appSettings)
        {
            // Globalização, mais utlizado em MVC tradicional

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Configurações do MVC

            services
                .AddMvc(options =>
                {
                    // Filter para vincular o Token ao usuário da Controller (Ticket)
                    options.Filters.Add<AccountTicketBinderFilter>();

                    // Ref.: @ValidateViewModel
                    options.Filters.Add<ValidateModelFilter>();

                    // Ref.: @ErrorHandler
                    options.Filters.Add<ExceptionFilter>();

                    // Cache
                    options.CacheProfiles.Add("Default",
                        new CacheProfile()
                        {
                            Duration = appSettings.Cache.Duration
                        });

                    // Exemplo de como mudar as mensagens de validação padrão

                    options.ModelBindingMessageProvider
                        .SetValueMustNotBeNullAccessor(
                            (_) => Messages.Required
                        );
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void ConfigureJwt
            (this IServiceCollection services, AppSettings appSettings)
        {
            // Ref.: @JwtCustom

            var key = Encoding.ASCII.GetBytes(appSettings.Security.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static void ConfigureCompression(this IApplicationBuilder app)
        {
            // Ref: @ResponseCompression

            app.UseResponseCompression();
        }

        public static void ConfigureApi(this IApplicationBuilder app)
        {
            // Políticas de CORS

            app.UseCors("Default");
        }

        public static void ConfigureMvc(this IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public static void ConfigureJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
