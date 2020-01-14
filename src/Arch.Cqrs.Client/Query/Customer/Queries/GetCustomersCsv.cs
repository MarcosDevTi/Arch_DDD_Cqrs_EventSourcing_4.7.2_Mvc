using Arch.CqrsClient.Query.Customer.Models;
using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.CqrsClient.Query.Customer.Queries
{
    public class GetCustomersCsv : IQuery<IEnumerable<CustomerIndex>>
    {
        public string[] Properties { get; set; }
        public string Order { get; set; }
    }
}
