using Arch.Infra.Shared.Cqrs.Query;
using System;
using System.Collections.Generic;

namespace Arch.CqrsClient.Query.Customer.Queries
{
    public class GetCustomerHistory : IQuery<IReadOnlyList<object>>
    {
        public Guid AggregateId { get; set; }
    }
}
