using Autofac;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.Reflection;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.RestApi.HostingExtensions.AutofacModules
{
    public sealed class AplicacaoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyAtual = Assembly.GetExecutingAssembly().GetName().Name;
            var assemblies = DependencyContext.Default.RuntimeLibraries
                                .Where(a => a.Name.StartsWith("MyCompany") && a.Name != assemblyAtual)
                                .Select(a => Assembly.Load(new AssemblyName(a.Name)))
                                .ToArray();
            
            builder
                .RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}