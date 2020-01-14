using System;

namespace Arch.CqrsClient.Event.Product
{
    public class ProductDeleted : Infra.Shared.Cqrs.Event.Event
    {
        public ProductDeleted(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
    }
}
