using Arch.CqrsClient.Command.Customer;
using Arch.CqrsClient.Query.Customer.Models;
using Arch.CqrsClient.Query.Customer.Queries;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using Arch.Infra.Shared.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arch.CqrsHandlers.Customer
{
    public class CustomerQueryHandler :
        IQueryHandler<GetCustomerHistory, IReadOnlyList<object>>,
        IQueryHandler<GetCustomersPaging, PagedResult<CustomerIndex>>,
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

        public PagedResult<CustomerIndex> Handle(GetCustomersPaging query) =>
            _architectureContext.Customers.Include(_ => _.Address).GetPagedResult<Domain.Models.Customer, CustomerIndex>(query.Paging);

        public UpdateCustomer Handle(GetCustomerForUpdate query) =>
            Mapper.Map<UpdateCustomer>(_architectureContext.Customers.Include(_ => _.Address).FirstOrDefault(_ => _.Id == query.Id));
    }
}
