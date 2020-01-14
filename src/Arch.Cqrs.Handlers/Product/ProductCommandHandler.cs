using Arch.Cqrs.Client.Command.ProductOnlyCqrs;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;

namespace Arch.Cqrs.Handlers.Product
{
    public class ProductCommandHandler :
        ICommandHandler<CreateProduct>
    {
        private readonly ArchCoreContext _context;
        public ProductCommandHandler(ArchCoreContext context)
        {
            _context = context;
        }
        public void Handle(CreateProduct command)
        {
            _context.Add(new Domain.Models.Product(command.Name, command.Description, command.Price));
            _context.SaveChanges();
        }
    }
}
