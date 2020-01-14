using Arch.CqrsClient.Query.Customer.Models;
using Arch.Infra.Shared.Cqrs.Query;
using System;

namespace Arch.CqrsClient.Query.Customer.Queries
{
    public class GetCustomerDetails : IQuery<CustomerDetails>
    {
        public GetCustomerDetails(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
}
