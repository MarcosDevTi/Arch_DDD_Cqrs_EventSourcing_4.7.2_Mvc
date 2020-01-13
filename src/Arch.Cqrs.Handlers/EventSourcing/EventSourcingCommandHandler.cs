using Arch.Cqrs.Client.Command.EventSourcing;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Cqrs.Handlers.EventSourcing
{
    public class EventSourcingCommandHandler :
        ICommandHandler<TruncateEventSourcing>
    {
        private readonly EventSourcingCoreContext _eventSourcingContext;
        public EventSourcingCommandHandler(EventSourcingCoreContext eventSourcingContext)
        {
            _eventSourcingContext = eventSourcingContext;
        }
        public void Handle(TruncateEventSourcing command)
        {
            _eventSourcingContext.EventEntities.RemoveRange(_eventSourcingContext.EventEntities);
            _eventSourcingContext.SaveChanges();
        }
    }
}
