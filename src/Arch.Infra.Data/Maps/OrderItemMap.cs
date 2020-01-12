using Arch.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arch.Infra.Data.Maps
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            //Property(_ => _.CreatedDate).HasColumnType("datetime2");
            //HasOne(i => i.Product);
        }

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
