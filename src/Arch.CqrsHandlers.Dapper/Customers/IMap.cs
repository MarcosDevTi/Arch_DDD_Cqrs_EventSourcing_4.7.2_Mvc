using System;

namespace Arch.CqrsHandlers.Dapper.Customers
{
    public interface IMap<TSource, TDestination>
    {
        void Add<TSourceMember, TDestinationMember>(Func<TSource, TSourceMember> source, Func<TDestination, TDestinationMember> destination);
    }
}
