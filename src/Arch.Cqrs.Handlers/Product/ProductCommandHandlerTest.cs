using Arch.Cqrs.Client.Command.ProductTest;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Handlers.Product
{
    public class ProductCommandHandlerTest:
        ICommandHandler<CreerProduct>
    {
        private readonly ArchCoreContext _context;
        public ProductCommandHandlerTest(ArchCoreContext context)
        {
            _context = context;
        }

        public void Handle(CreerProduct command)
        {
            _context.Add(new Domain.Models.Product(command.Name, command.Description, command.Price));
            _context.SaveChanges();
        }
    }
}
