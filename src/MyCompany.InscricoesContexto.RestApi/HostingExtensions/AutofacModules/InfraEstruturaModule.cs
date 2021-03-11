using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyCompany.InscricoesContexto.Infraestrutura;

namespace MyCompany.InscricoesContexto.RestApi.HostingExtensions.AutofacModules
{
    public class InfraEstruturaModule : Autofac.Module
    {
        protected void Load(ContainerBuilder builder, IConfiguration configuration)
        {
            builder
                .Register(x=>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<InscricoesDbContext>();
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("Inscricoes"));
                    return new InscricoesDbContext(optionsBuilder.Options);
                })
                .InstancePerLifetimeScope();
        }
    }
}