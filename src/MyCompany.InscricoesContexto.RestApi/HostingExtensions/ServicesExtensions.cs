using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyCompany.InscricoesContexto.RestApi.HostingExtensions.Filtros;

namespace MyCompany.InscricoesContexto.RestApi.HostingExtensions
{
    internal static class ServicesExtensions
    {
        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
            => services.AddApplicationInsightsTelemetry(configuration);

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .SetIsOriginAllowed((host) => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(configuration.GetValue("OrigensPermitidas", "*")));
            });
            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder
                .AddSqlServer(
                    configuration["ConnectionStrings:Tenants"],
                    name: "financeiro-check",
                    tags: new string[] { "FinanceiroDbCheck" });
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}