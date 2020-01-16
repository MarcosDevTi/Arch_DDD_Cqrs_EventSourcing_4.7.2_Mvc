using Arch.CqrsClient.Command.Customer;
using Arch.Domain.Models;
using System;

namespace Arch.CqrsHandlers.Dapper.Customers
{
    public class Map
    {
        public void Add<TSourceMember, TDestinationMember>(Func<CreateCustomer, TSourceMember> source, Func<Customer, TDestinationMember> destination)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            Add(_ => _.City, _ => _.Address);
        }
    }
}
