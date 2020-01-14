using Arch.CqrsClient.Command.Customer;
using Arch.Infra.Shared.Cqrs.Query;
using System;

namespace Arch.CqrsClient.Query.Customer.Queries
{
    public class GetCustomerForUpdate : IQuery<UpdateCustomer>
    {
        public GetCustomerForUpdate(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
