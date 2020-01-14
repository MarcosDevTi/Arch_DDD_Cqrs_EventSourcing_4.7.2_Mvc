﻿using System;
using Arch.CqrsClient.AutoMapper;
using Arch.CqrsClient.Command.Product;

namespace Arch.CqrsClient.Event.Product
{
    public class ProductUpdated : Infra.Shared.Cqrs.Event.Event, IMapFrom<UpdateProduct>
    {
        public ProductUpdated() {}
        public ProductUpdated(Guid id, string name, string description, decimal price)
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
    }
}
