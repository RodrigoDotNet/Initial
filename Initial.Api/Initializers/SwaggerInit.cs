using System;
using System.Collections.Generic;
using System.Reflection;
using Initial.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Initial.Api.Initializers
{
    /// <summary>
    /// Configuração do Swagger, responsável para UI da Web Api
    /// </summary>
    public static class SwaggerInit
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider()
                .GetRequiredService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                // Identificação das versões implementadas

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName,
                        new Info()
                        {
                            Title = $"Initial API {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = "Documentation.",
                            TermsOfService = "None",
                            Contact = new Contact
                            {
                                Name = "Paulo",
                                Email = "paulo@bpmobi.com",
                                Url = "http://www.bpmobi.com"
                            }
                        });
                }

                // Para incluir os exemplos na documentação

                options.OperationFilter<ExamplesOperationFilter>();

                // Filter para ocultar ações não autenticadas.

                options.OperationFilter<AuthorizeCheckOperationFilter>();

                options.DocumentFilter<AuthorizeCheckOperationFilter>();

                // Incluir os comentários do código à documentação

                var xmlCommentsPath = Assembly.GetExecutingAssembly()
                    ?.Location
                    ?.Replace("dll", "xml");

                if (!string.IsNullOrEmpty(xmlCommentsPath))
                    options.IncludeXmlComments(xmlCommentsPath);

                // Configuração do JWT

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", Array.Empty<string>()},
                };

                options.AddSecurityDefinition(
                    "Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Inform 'Bearer + token'",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                options.AddSecurityRequirement(security);
            });
        }

        public static void ConfigureSwagger
            (this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );

                    options.RoutePrefix = string.Empty;

                    options.DocumentTitle = "Initial API Documentation";

                    options.DocExpansion(DocExpansion.None);
                }
            });
        }
    }
}