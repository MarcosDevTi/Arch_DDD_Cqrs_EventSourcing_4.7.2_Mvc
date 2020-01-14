using System;

namespace Arch.CqrsClient.Event.OrderItem
{
    public class OrderItemDeleted : Infra.Shared.Cqrs.Event.Event
    {
        public OrderItemDeleted(Guid id) => Id = id;

        public Guid Id { get; private set; }
    }
}
