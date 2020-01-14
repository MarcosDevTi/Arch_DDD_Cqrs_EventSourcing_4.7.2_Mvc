
using Arch.CqrsClient.Query.Customer.Models;
using Arch.Infra.Shared.Cqrs.Query;
using Arch.Infra.Shared.Pagination;

namespace Arch.CqrsClient.Query.Customer.Queries
{
    public class GetCustomersPaging : IQuery<PagedResult<CustomerIndex>>
    {
        public GetCustomersPaging(Paging paging) => Paging = paging;

        public Paging Paging { get; set; }
    }
}
