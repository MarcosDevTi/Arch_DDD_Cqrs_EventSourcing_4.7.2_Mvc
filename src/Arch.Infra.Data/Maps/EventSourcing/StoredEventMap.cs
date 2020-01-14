using Arch.Domain.Core.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arch.Infra.Data.Maps.EventSourcing
{
    public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>
    {


        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(c => c.When)
                .HasColumnName("When");

            builder.Property(c => c.Action)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");
        }
    }
}
