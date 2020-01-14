using Arch.CqrsClient.Command.EventSourcing;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;

namespace Arch.CqrsHandlers.EventSourcing
{
    public class EventSourcingCommandHandler :
        ICommandHandler<TruncateEventSourcing>
    {
        private readonly EventSourcingCoreContext _eventSourcingContext;

        public EventSourcingCommandHandler(EventSourcingCoreContext eventSourcingContext) =>
            _eventSourcingContext = eventSourcingContext;

        public void Handle(TruncateEventSourcing command)
        {
            _eventSourcingContext.EventEntities.RemoveRange(_eventSourcingContext.EventEntities);
            _eventSourcingContext.SaveChanges();
        }
    }
}
