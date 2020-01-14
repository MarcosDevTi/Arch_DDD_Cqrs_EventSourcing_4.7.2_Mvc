using Arch.CqrsClient.Command.OrderItem;
using Arch.Domain.Models;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Arch.CqrsHandlers.OderItem
{
    public class OrderItemCommandHandler :
        ICommandHandler<CreateOrderItem>
    {
        private readonly ArchCoreContext _architectureContext;

        public OrderItemCommandHandler(ArchCoreContext architectureContext)
        {
            _architectureContext = architectureContext;
        }

        public void Handle(CreateOrderItem command)
        {
            var product = _architectureContext.Products.Find(command.ProductId);

            var orderItem = _architectureContext.OrderItems.Include(x => x.Product)
                .FirstOrDefault(x => x.Product.Id == command.ProductId);

            if (orderItem == null)
            {
                _architectureContext.OrderItems.Add(new OrderItem(product));
            }
            else
            {
                orderItem.Qtd++;
                //_architectureContext.OrderItems.Update(orderItem);
            }

            _architectureContext.SaveChanges();
        }
    }
}
