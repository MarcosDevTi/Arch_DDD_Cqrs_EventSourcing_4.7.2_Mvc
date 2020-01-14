using System;

namespace Arch.Cqrs.Client.ProductOnlyCqrs
{
    public class ProductItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
