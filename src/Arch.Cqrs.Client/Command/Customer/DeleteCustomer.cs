using Arch.Cqrs.Client.Command.Customer.Validation;
using System;

namespace Arch.Cqrs.Client.Command.Customer
{
    public class DeleteCustomer : CustomerCommand
    {
        public Guid Id { get; set; }

        public override bool IsValid() => true;
    }
}
