using Arch.Infra.Shared.EventSourcing;

namespace Arch.CqrsClient.Event.Customer
{
    public class CustomerEventMap : SourceFluent<CustomerCreated>
    {
        public override void Configuration(SourceFluent<CustomerCreated> builder)
        {
            builder.Ignore(_ => _.Email, _ => _.BirthDate);
            builder.DisplayName(_ => _.BirthDate, "Date Naissance");
        }
    }

    public class CustomerUpdatedMap : SourceFluent<CustomerUpdated>
    {
        public override void Configuration(SourceFluent<CustomerUpdated> builder)
        {
            builder.Ignore(_ => _.Email, _ => _.BirthDate);
            builder.DisplayName(_ => _.BirthDate, "Date Naissance");
        }
    }
}
