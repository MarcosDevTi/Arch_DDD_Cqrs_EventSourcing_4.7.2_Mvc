using Arch.Infra.Shared.Cqrs.Contracts;
using System;

namespace Arch.CqrsClient.Command.OrderItem
{
    public class DeleteOrderItem : CommandAction
    {
        public DeleteOrderItem(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override bool IsValid() => true;
    }
}
