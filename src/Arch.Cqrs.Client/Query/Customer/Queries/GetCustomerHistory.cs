using System;
using System.Collections.Generic;
using Arch.Infra.Shared.Cqrs.Query;

namespace Arch.Cqrs.Client.Query.Customer.Queries
{
    public class GetCustomerHistory: IQuery<IReadOnlyList<object>>
    {
        public Guid AggregateId { get; set; }
    }
}
