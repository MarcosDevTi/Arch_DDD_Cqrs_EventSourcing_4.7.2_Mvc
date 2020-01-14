using Arch.Infra.Shared.Cqrs.Contracts;
using System;

namespace Arch.CqrsClient.Command.OrderItem
{
    public class CreateOrderItem : CommandAction
    {
        public CreateOrderItem()
        {

        }
        public CreateOrderItem(Guid productId, int qtd)
        {
            ProductId = productId;
            Qtd = qtd;
        }
        public Guid ProductId { get; set; }
        public int Qtd { get; set; }

        public override bool IsValid() => true;
    }
}
