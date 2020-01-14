using Arch.Infra.Shared.Cqrs.Commands;

namespace Arch.Cqrs.Client.Command.ProductOnlyCqrs
{
    public class CreateProduct : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
