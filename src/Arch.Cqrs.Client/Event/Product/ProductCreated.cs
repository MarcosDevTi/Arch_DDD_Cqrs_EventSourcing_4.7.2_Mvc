using System;
using Arch.CqrsClient.AutoMapper;
using Arch.CqrsClient.Command.Product;
using AutoMapper;

namespace Arch.CqrsClient.Event.Product
{
    public class ProductCreated : Infra.Shared.Cqrs.Event.Event, ICustomMapper
    {
        public ProductCreated() {}
        public ProductCreated(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public void Map(IMapperConfigurationExpression cfg) =>
            cfg.CreateMap<CreateProduct, ProductCreated>()
                .ConstructUsing(x => new ProductCreated(x.Id, x.Name, x.Description, x.Price))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}
