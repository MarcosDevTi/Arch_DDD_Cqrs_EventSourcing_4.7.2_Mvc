﻿using Arch.CqrsClient.AutoMapper;
using Arch.Infra.Shared.Cqrs.Contracts;
using Arch.Infra.Shared.Grid;
using AutoMapper;
using System;

namespace Arch.CqrsClient.Query.Customer.Models
{
    public class CustomerIndex : CommandAction, ICustomMapper
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Score { get; set; }
        public string Address { get; set; }

        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Domain.Models.Customer, CustomerIndex>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address.Number + " " + s.Address.Street + " " + s.Address.City));

            cfg.CreateMap<CustomerIndex, Domain.Models.Customer>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name));
        }

        public override bool IsValid() => true;
    }

    public class CustomerIndexMapGrid : GridFluent<CustomerIndex>
    {
        public override void Configuration(GridFluent<CustomerIndex> builder) => builder
                .AddGridMember(_ => _.Name, true, "Nom")
                .AddGridMember(_ => _.BirthDate, true, "Date de Naissance")
                .AddGridMember(_ => _.Email, true, "E-mail")
                .AddGridMember(_ => _.Score, true, "Resultat");
    }
}
