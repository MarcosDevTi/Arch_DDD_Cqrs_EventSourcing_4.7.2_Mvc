using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.Cqrs.Client.Query.Generics
{
    public class GetEntities : IQuery<IEnumerable<EntityByUser>>
    {
    }

    public class GetEntityByUserId : IQuery<IEnumerable<EntityByUser>>
    {
        public string UserId { get; set; }
    }
}
