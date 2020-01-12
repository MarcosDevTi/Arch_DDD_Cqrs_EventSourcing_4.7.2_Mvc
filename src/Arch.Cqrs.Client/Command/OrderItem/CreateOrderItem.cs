using System;

namespace Arch.Cqrs.Client.Command.OrderItem
{
    public class CreateOrderItem : Infra.Shared.Cqrs.Commands.Command
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
