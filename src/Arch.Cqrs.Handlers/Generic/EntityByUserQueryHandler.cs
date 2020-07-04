using Arch.Cqrs.Client.Query.Generics;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;
using System.Linq;

namespace Arch.Cqrs.Handlers.Generic
{
    public class EntityByUserQueryHandler :
        IQueryHandler<GetEntityByUserId, IEnumerable<EntityByUser>>
    {
        private readonly EventSourcingCoreContext _eventSourcingContext;

        public EntityByUserQueryHandler(EventSourcingCoreContext eventSourcingContext)
        {
            _eventSourcingContext = eventSourcingContext;
        }

        public IEnumerable<EntityByUser> Handle(GetEntityByUserId query)
        {
            var events = _eventSourcingContext.EventEntities
                .Where(u => u.Who == query.UserId).GroupBy(g => g.AggregateId).ToList()
                .Select(_ =>
                {
                    var ev = _eventSourcingContext.EventEntities
                        .Where(w => w.AggregateId == _.Key)
                        .OrderBy(o => o.When).FirstOrDefault();
                    return new EntityByUser
                    {
                        AggregatedId = _.Key,
                        When = ev?.When,
                        Who = ev?.Who,
                        Action = ev?.Action
                    };
                });

            return events.ToList();
        }
    }
}
