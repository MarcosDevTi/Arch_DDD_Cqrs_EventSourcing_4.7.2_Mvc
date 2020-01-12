﻿using System;
using Arch.Domain.Core;

namespace Arch.Domain.ValueObjects
{
    public class Address: Entity
    {
        public Address()
        {

        }
        public Address(string street, string number, string city, string zipCode, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
        }

        public Address Update(string street, string number, string zipCode)
        {
            Street = street;
            Number = number;
            ZipCode = zipCode;
            return this;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
    }
}
