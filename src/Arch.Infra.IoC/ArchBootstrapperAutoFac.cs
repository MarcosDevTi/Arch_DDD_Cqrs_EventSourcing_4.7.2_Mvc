using Arch.CqrsClient.AutoMapper;
using Arch.CqrsClient.Command.Customer;
using Arch.CqrsHandlers.Customer;
using Arch.Domain;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Event;
using Arch.Infra.Data;
using Arch.Infra.Data.EventSourcing;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.Cqrs.Extentions;
using Arch.Mvc.Models;
using Autofac;

namespace Arch.Infra.IoC
{
    public class ArchBootstrapperAutoFac
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().SingleInstance();
            AutoMapperConfig.Register<CreateCustomer>();
            builder.RegisterType<Test>().As<ITest>();

            builder.RegisterType<Processor>().As<IProcessor>().InstancePerRequest();
            builder.RegisterType<ArchCoreContext>().InstancePerRequest();
            builder.RegisterType<EventSourcingCoreContext>().InstancePerRequest();
            builder.AddCqrsAutoFac<CustomerCommandHandler>();
            builder.RegisterType<DomainNotificationHandler>().As<IDomainNotification>().InstancePerRequest();
            builder.RegisterType<EventRespoitory>().As<IEventRepository>().InstancePerRequest();
            builder.RegisterType<DapperContext>();
        }
    }
}
