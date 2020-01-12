using Arch.Cqrs.Client.AutoMapper;
using Arch.Cqrs.Client.Command.Customer.Validation;
using Arch.Domain.ValueObjects;
using AutoMapper;
using System;

namespace Arch.Cqrs.Client.Command.Customer
{
    public class UpdateCustomer : CustomerCommand, ICustomMapper
    {
        public UpdateCustomer()
        {

        }
        public Guid Id { get; set; }
        public UpdateAddress UpdateAddress { get; set; }
        public UpdateCustomer(Guid id, string firstName, string lastName, string email, DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }

        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UpdateCustomer, Domain.Models.Customer>()
                .ConstructUsing(c => new Domain.Models.Customer(
                    c.FirstName,
                    c.LastName,
                    c.Email,
                    c.BirthDate,
                    c.Id
                    ))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s =>
                             new Address().Update(s.Street, s.Number, s.ZipCode)))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            cfg.CreateMap<Domain.Models.Customer, UpdateCustomer>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Address.Number))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.Address.ZipCode))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Address.Number))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.Address.ZipCode));
        }

        public override bool IsValid() => true;
    }
}
