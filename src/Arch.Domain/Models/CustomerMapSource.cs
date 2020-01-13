using Arch.Infra.Shared.EventSourcing;

namespace Arch.Domain.Models
{
    public class CustomerMapSource : SourceFluent<Customer>
    {
        public override void Configuration(SourceFluent<Customer> builder)
        {
            //builder.Ignore(_ => _.EmailAddress);
            builder
                .DisplayName(_ => _.FirstName, "Prénom")
                .DisplayName(_ => _.LastName, "Nom")
                .DisplayName(_ => _.EmailAddress, "Couriel")
                .DisplayName(_ => _.BirthDate, "Date naissance");
            //.Ignore(_ => _.BirthDate)
        }
    }
}