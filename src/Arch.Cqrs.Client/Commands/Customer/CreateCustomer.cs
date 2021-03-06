﻿using Arch.CqrsClient.AutoMapper;
using Arch.CqrsClient.Command.Customer.Validation;
using Arch.CqrsClient.Query.Customer.Models;
using Arch.Domain.ValueObjects;
using AutoMapper;
using FluentValidation.Results;
using System;

namespace Arch.CqrsClient.Command.Customer
{
    public class CreateCustomer : CustomerCommand, ICustomMapper
    {
        public CreateCustomer() { }
        public CreateCustomer(
            string firstName,
            string lastName,
            string email,
            DateTime birthDate,
            Guid? userId = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }

        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreateCustomer, Domain.Models.Customer>()
               .ConstructUsing(c =>
                   new Domain.Models.Customer(
                       c.FirstName, c.LastName, c.Email, c.BirthDate))
                       .ForMember(d => d.Address, opt => opt.MapFrom(s =>
                             new Address(s.Street, s.Number, s.City, s.ZipCode, null)))
               .IgnoreAllPropertiesWithAnInaccessibleSetter();

            cfg.CreateMap<CustomerIndex, UpdateCustomer>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
