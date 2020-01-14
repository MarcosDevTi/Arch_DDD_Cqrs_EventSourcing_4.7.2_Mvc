using Arch.CqrsClient.Query.OrderItem.Models;
using Arch.CqrsClient.Query.OrderItem.Queries;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Arch.CqrsHandlers.OderItem
{
    public class OderItemQueryHandler :
        IQueryHandler<GetOrderItensIndex, IReadOnlyList<OrderItemIndex>>,
        IQueryHandler<GetCart, Cart>
    {
        private readonly ArchCoreContext _architectureContext;

        public OderItemQueryHandler(ArchCoreContext architectureContext)
        {
            _architectureContext = architectureContext;
        }

        public IReadOnlyList<OrderItemIndex> Handle(GetOrderItensIndex query)
        {
            var ordersItens = from oi in _architectureContext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.OrderItems)
                              from o in oi.OrderItems
                                  //where oi.Customer.Id == _user.UserId()
                              select o;

            return ordersItens.Include(x => x.Product)
                .Select(x => new OrderItemIndex
                {
                    Id = x.Id,
                    Product = $"{x.Product.Name}, Price:{x.Product.Price}",
                    Qtd = x.Qtd
                }).ToList();
        }

        public Cart Handle(GetCart query)
        {
            var ordersItens = from oi in _architectureContext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.OrderItems)
                              from o in oi.OrderItems
                                  //where oi.Customer.Id == _user.UserId()
                              select o;

            var result = ordersItens.Include(x => x.Product)
                .Select(x => new OrderItemIndex
                {
                    Id = x.Id,
                    Product = $"{x.Product.Name}, Price:{x.Product.Price}",
                    Qtd = x.Qtd
                }).ToList();

            return new Cart
            {
                OrderItens = result,
                TotalPrice = ordersItens.Sum(x => x.Product.Price * x.Qtd)
            };
        }
    }
}
