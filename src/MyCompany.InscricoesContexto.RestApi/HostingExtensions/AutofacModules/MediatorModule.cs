using Autofac;
using MediatR;
using System.Reflection;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.RestApi.HostingExtensions.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var assembly = typeof(IAgreggateRoot).GetTypeInfo().Assembly;

            // Registrar todos os handlers para comandos de um assembly
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();

            // Registrar todos os handlers que devem fazer Retry para o banco dados de um assembly
            //builder.RegisterAssemblyTypes(typeof(AtualizarSaldoDiarioComando).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IRetryableRequest<,>))
            //    .InstancePerDependency();

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
