using Arch.Cqrs.Client.Command.Customer;
using Arch.Infra.Shared.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Client.Query.Customer.Queries
{
    public class GetCustomerForUpdate: IQuery<UpdateCustomer>
    {
        public GetCustomerForUpdate(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
