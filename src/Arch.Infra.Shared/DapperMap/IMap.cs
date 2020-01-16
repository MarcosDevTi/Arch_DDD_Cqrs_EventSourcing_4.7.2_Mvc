using System;

namespace Arch.Infra.Shared.Cqrs.DapperMap
{
    public abstract class Map<TSource, TDestination>
    {
        public abstract void Mapping();
        public void Add<TSourceMember, TDestinationMember>(Func<TSource, TSourceMember> source, Func<TDestination, TDestinationMember> destination)
        {

        }
    }
}
