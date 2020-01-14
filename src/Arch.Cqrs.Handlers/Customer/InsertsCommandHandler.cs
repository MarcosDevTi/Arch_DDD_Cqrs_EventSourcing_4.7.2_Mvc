using Arch.Cqrs.Handlers;
using Arch.CqrsClient.Command.Customer;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Event;
using Arch.Domain.ValueObjects;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using Bogus;
using System;
using System.Collections.Generic;

namespace Arch.CqrsHandlers.Customer
{
    public class InsertsCommandHandler : CommandHandler<Domain.Models.Customer>,
         ICommandHandler<InsertVolumeCustomers>,
        ICommandHandler<TrucateCustomers>
    {
        private readonly ArchCoreContext _architectureContext;

        public InsertsCommandHandler(
            ArchCoreContext architectureContext,
            IDomainNotification notifications,
            IEventRepository eventRepository,
            EventSourcingCoreContext eventSourcingContext) : base(architectureContext, notifications, eventRepository, eventSourcingContext)
        {
            _architectureContext = architectureContext;
        }

        public void Handle(InsertVolumeCustomers command)
        {
            var faker = new Faker();
            var list = new List<Domain.Models.Customer>();
            for (var i = 0; i < command.InsertsCount; i++)
            {
                var minDate = DateTime.Now.AddYears(-30);
                var maxDate = DateTime.Now.AddYears(-60);

                var customer = new Domain.Models.Customer(
                    faker.Name.FirstName(),
                    faker.Name.LastName(),
                    faker.Person.Email,
                    faker.Date.Between(minDate, maxDate));
                customer.Score = faker.Random.Int(0, 100);

                var address = new Address(faker.Address.StreetName(), faker.Address.BuildingNumber(), faker.Address.City(), faker.Address.ZipCode());
                customer.Address = address;

                list.Add(customer);
            }

            list.ForEach(_ => _architectureContext.Add(_));
            _architectureContext.SaveChanges();
        }

        public void Handle(TrucateCustomers command)
        {
            _architectureContext.Customers.RemoveRange(_architectureContext.Customers);
            _architectureContext.Addresses.RemoveRange(_architectureContext.Addresses);
            _architectureContext.SaveChanges();
        }

        public List<Domain.Models.Customer> GetCustomers(int interactions)
        {
            var faker = new Faker();
            var list = new List<Domain.Models.Customer>();
            for (var i = 0; i < interactions; i++)
            {
                var minDate = DateTime.Now.AddYears(-30);
                var maxDate = DateTime.Now.AddYears(-60);

                var idAddress = Guid.NewGuid();
                var address = new Address(faker.Address.StreetName(), faker.Address.BuildingNumber(), faker.Address.City(), faker.Address.ZipCode(), idAddress);
                var customer = new Domain.Models.Customer(
                    faker.Name.FirstName(),
                    faker.Name.LastName(),
                    faker.Person.Email,
                    faker.Date.Between(minDate, maxDate)
                    );
                customer.Score = faker.Random.Int();

                customer.Address = address;
                customer.AddressId = idAddress;

                list.Add(customer);
            }

            return list;
        }
    }
}
