﻿using Arch.CqrsClient.AutoMapper;
using Arch.CqrsClient.Command.Customer;
using Arch.CqrsHandlers.Customer;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Event;
using Arch.Infra.Data;
using Arch.Infra.Data.EventSourcing;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.Cqrs.Extentions;
using SimpleInjector;

namespace Arch.Infra.IoC
{
    public class ArchBootstrapper
    {
        public static Container MyContainer { get; set; }

        public static void Register(Container container)
        {
            MyContainer = container;
            AutoMapperConfig.Register<CreateCustomer>();

            container.Register<IProcessor, Processor>(Lifestyle.Transient);
            container.Register<ArchCoreContext>(Lifestyle.Scoped);
            container.Register<EventSourcingCoreContext>(Lifestyle.Transient);
            container.AddCqrs<CustomerCommandHandler>();
            container.Register<IDomainNotification, DomainNotificationHandler>(Lifestyle.Scoped);
            container.Register<IEventRepository, EventRespoitory>(Lifestyle.Transient);

        }
    }
}
