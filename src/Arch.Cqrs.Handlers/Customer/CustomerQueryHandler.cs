using System;
using System.Collections.Generic;
using System.Linq;
using Arch.Cqrs.Client.Command.Customer;
using Arch.Cqrs.Client.Paging;
using Arch.Cqrs.Client.Query.Customer.Models;
using Arch.Cqrs.Client.Query.Customer.Queries;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Arch.Paging;
using Microsoft.EntityFrameworkCore;

namespace Arch.Cqrs.Handlers.Customer
{
    public class CustomerQueryHandler :
        IQueryHandler<GetCustomerHistory, IReadOnlyList<object>>,
        IQueryHandler<GetCustomersPaging, Paging.PagedResult<CustomerIndex>>,
        IQueryHandler<GetCustomerForUpdate, UpdateCustomer>
    {
        private readonly ArchCoreContext _architectureContext;
        private readonly EventSourcingCoreContext _eventSourcingContext;

        public CustomerQueryHandler(ArchCoreContext architectureContext, EventSourcingCoreContext eventSourcingContext)
        {
            _architectureContext = architectureContext;
            _eventSourcingContext = eventSourcingContext;
        }

        public CustomerDetails Handle(GetCustomerDetails query) =>
            Mapper.Map<CustomerDetails>(_architectureContext.Customers.Find(query.Id));

        public IReadOnlyList<object> Handle(GetCustomerHistory query) => 
            GetEventSourcingEvent<Domain.Models.Customer>(query.AggregateId).ToList();

        protected IEnumerable<object> GetEventSourcingEvent<T>(Guid aggregateId) =>
           _eventSourcingContext.EventEntities.Where(x =>
                   x.AggregateId == aggregateId)
                   .OrderBy(d => d.When)
                   .ToList().Select(_ => _.ReadToObject(_, typeof(T))
               ).ToList();

        public IEnumerable<CustomerIndex> Handle(GetCustomersCsv query) =>
            _architectureContext.Customers.ProjectTo<CustomerIndex>().ToList();

        public Paging.PagedResult<CustomerIndex> Handle(GetCustomersPaging query) =>
            _architectureContext.Customers.Include(_ => _.Address).GetPagedResult<Domain.Models.Customer, CustomerIndex>(query.Paging);

        public UpdateCustomer Handle(GetCustomerForUpdate query) =>
            Mapper.Map<UpdateCustomer>(_architectureContext.Customers.Include(_ => _.Address).FirstOrDefault(_ => _.Id == query.Id));
    }
}
