using Arch.Cqrs.Handlers;
using Arch.CqrsClient.Command.Customer;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Event;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Arch.CqrsHandlers.Customer
{
    public class CustomerCommandHandler : CommandHandler<Domain.Models.Customer>,
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>,
        ICommandHandler<DeleteCustomer>
    {
        private readonly ArchCoreContext _architectureContext;

        public CustomerCommandHandler(
            ArchCoreContext architectureContext,
            IDomainNotification notifications,
            IEventRepository eventRepository,
            EventSourcingCoreContext eventSourcingContext) : base(architectureContext, notifications, eventRepository, eventSourcingContext)
        {
            _architectureContext = architectureContext;
        }

        public void Handle(CreateCustomer command)
        {
            ValidateCommand(command);

            var customer = Mapper.Map<Domain.Models.Customer>(command);
            var exists = ExistsValidation(x =>
                x.EmailAddress == customer.EmailAddress, command.Action, "The customer e-mail has already been taken.");
            if (exists) { return; }

            var action = _architectureContext.AddEntity(customer);
            command.AggregateId = customer.Id;

            Commit(customer, command, action);
        }

        public void Handle(UpdateCustomer command)
        {
            ValidateCommand(command);
            command.AggregateId = command.Id;

            var customerTrackerd = _architectureContext.Customers.Include(_ => _.Address).FirstOrDefault(_ => _.Id == command.Id);
            var customer = Mapper.Map(command, customerTrackerd);
            customer.UpdateAddress(command.Street, command.Number, command.ZipCode);

            ExistsValidation(x =>
                x.EmailAddress == customer.EmailAddress && x.Id != command.Id, command.Action, "The customer e-mail has already been taken.");

            var lastEntity = Db().Include(_ => _.Address).AsNoTracking()
                .OrderBy(_ => _.CreatedDate).FirstOrDefault(_ => _.Id == command.Id);

            var action = _architectureContext.UpdateEntity(customer);
            var lastCommand = Mapper.Map(lastEntity, lastEntity.GetType(), command.GetType());
            Commit(customer, command, action, lastCommand);
        }

        public void Handle(DeleteCustomer command)
        {
            ValidateCommand(command);
            var lastEntity = Db().Include(_ => _.Address).AsNoTracking()
               .OrderBy(_ => _.CreatedDate).FirstOrDefault(_ => _.Id == command.Id);
            var entity = Db().Include(_ => _.Address).FirstOrDefault(_ => _.Id == command.Id);
            var address = _architectureContext.Addresses.Find(entity.Address.Id);

            var action = _architectureContext.DeleteEntity(entity);
            Commit(entity, action, lastEntity);
        }

        private Arch.Domain.Models.Customer GetById(Guid id) => Db().Find(id);
    }
}
