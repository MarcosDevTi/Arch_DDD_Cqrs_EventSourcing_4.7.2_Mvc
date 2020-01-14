using Arch.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arch.Infra.Data.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Property(_ => _.CreatedDate).HasColumnType("datetime2");

        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.OrderItems);
        }
    }
}
