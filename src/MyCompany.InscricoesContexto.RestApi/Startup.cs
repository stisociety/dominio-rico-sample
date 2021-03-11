using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MyCompany.InscricoesContexto.RestApi.HostingExtensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.InscricoesContexto.Infraestrutura;
using MyCompany.InscricoesContexto.RestApi.HostingExtensions.AutofacModules;

namespace MyCompany.InscricoesContexto.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<InscricoesDbContext>(builder =>
                {
                    builder.UseSqlServer(Configuration.GetConnectionString("Inscricoes")); 
                })
                .AddApplicationInsights(Configuration)
                .AddHttpClients()
                .AddMemoryCache()
                .AddCustomMvc(Configuration)
                .AddHealthChecks(Configuration)
                .AddCustomIntegrations(Configuration);
            
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new InfraEstruturaModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new AplicacaoModule());
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                loggerFactory
                    .CreateLogger<Startup>()
                    .LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}