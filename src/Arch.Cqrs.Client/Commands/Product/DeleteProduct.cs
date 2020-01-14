using System;

namespace Arch.CqrsClient.Command.Product
{
    public class DeleteProduct : ProductCommand
    {
        public DeleteProduct(Guid id)
        {
            Id = id;
        }

        public override bool IsValid() => true;
    }
}
