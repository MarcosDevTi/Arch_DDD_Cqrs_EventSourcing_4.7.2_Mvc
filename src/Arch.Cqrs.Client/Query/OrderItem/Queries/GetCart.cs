using Arch.CqrsClient.Query.OrderItem.Models;
using Arch.Infra.Shared.Cqrs.Query;

namespace Arch.CqrsClient.Query.OrderItem.Queries
{
    public class GetCart : IQuery<Cart>
    {
    }
}
