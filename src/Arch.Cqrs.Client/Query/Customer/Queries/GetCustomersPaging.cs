
using Arch.Cqrs.Client.Query.Customer.Models;
using Arch.Infra.Shared.Cqrs.Query;
using Arch.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Client.Query.Customer.Queries
{
    public class GetCustomersPaging: IQuery<PagedResult<CustomerIndex>>
    {
        public GetCustomersPaging(Arch.Paging.Paging paging) => Paging = paging;

        public Arch.Paging.Paging Paging { get; set; }
    }
}
