using Arch.Cqrs.Client.ProductOnlyCqrs;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;
using System.Linq;

namespace Arch.Cqrs.Handlers.Product
{
    public class ProductQueryHandler : IQueryHandler<GetProductsList, IEnumerable<ProductItem>>
    {
        private readonly ArchCoreContext _context;
        public ProductQueryHandler(ArchCoreContext context) => _context = context;

        public IEnumerable<ProductItem> Handle(GetProductsList query) =>
             _context.Products.Take(query.Take)
                .Select(_ =>
                        new ProductItem
                        {
                            Id = _.Id,
                            Name = _.Name,
                            Description = _.Description,
                            Price = _.Price
                        });
    }
}
