using System;

namespace Arch.CqrsClient.Command.Order
{
    public class CreateOrder
    {
        public Guid CustomerId { get; set; }
    }
}
