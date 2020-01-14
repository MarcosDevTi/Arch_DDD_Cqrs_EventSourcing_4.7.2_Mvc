using Arch.CqrsClient.Query.OrderItem.Models;
using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.CqrsClient.Query.OrderItem.Queries
{
    public class GetOrderItensIndex : IQuery<IReadOnlyList<OrderItemIndex>>
    {
    }
}
