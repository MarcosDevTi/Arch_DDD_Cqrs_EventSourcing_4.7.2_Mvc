using Arch.Infra.Shared.Cqrs.Contracts;
using System;

namespace Arch.CqrsClient.Command.Cart
{
    public class AddItemCart : CommandAction
    {
        public AddItemCart(Guid orderItemId, int value)
        {
            Value = value;
            OrderItemId = orderItemId;
        }

        public int Value { get; private set; }

        public Guid OrderItemId { get; private set; }

        public override bool IsValid() => true;
    }
}
