using Arch.Cqrs.Client.Query.Product.Models;
using Arch.Cqrs.Client.Query.Product.Queries;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Handlers.Product
{
    public class ProductQueryHandler :
        IQueryHandler<GetProductsTest, IReadOnlyList<ProductIndex>>
    {
        private readonly ArchCoreContext _context;
        public ProductQueryHandler(ArchCoreContext context)
        {
            _context = context;
        }
        public IReadOnlyList<ProductIndex> Handle(GetProductsTest query)
        {
            return _context.Products.Take(query.Take).ProjectTo<ProductIndex>().ToList();
        }
    }
}
