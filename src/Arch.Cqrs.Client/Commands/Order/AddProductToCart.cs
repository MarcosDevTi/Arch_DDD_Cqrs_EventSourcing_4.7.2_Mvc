using Arch.Infra.Shared.Cqrs.Contracts;
using System;

namespace Arch.CqrsClient.Command.Order
{
    public class AddProductToCart : CommandAction
    {
        public AddProductToCart(Guid productId, Guid userId)
        {
            ProductId = productId;
            UserId = userId;
        }

        public Guid ProductId { get; private set; }
        public Guid UserId { get; private set; }

        public override bool IsValid() => true;
    }
}
