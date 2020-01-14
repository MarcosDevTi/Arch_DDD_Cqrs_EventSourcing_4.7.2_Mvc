using System;

namespace Arch.CqrsClient.Command.Customer
{
    public class DeleteCustomer : CustomerCommand
    {
        public Guid Id { get; set; }

        public override bool IsValid() => true;
    }
}
