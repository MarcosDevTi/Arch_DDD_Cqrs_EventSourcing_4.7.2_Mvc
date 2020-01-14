using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.Cqrs.Client.ProductOnlyCqrs
{
    public class GetProductsList : IQuery<IEnumerable<ProductItem>>
    {
        public GetProductsList(int take)
        {
            Take = take;
        }
        public int Take { get; set; }
    }
}
