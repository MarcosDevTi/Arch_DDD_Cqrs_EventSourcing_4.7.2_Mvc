﻿using Arch.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arch.Infra.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            //Property(_ => _.CreatedDate).HasColumnType("datetime2");


        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired()
               .HasMaxLength(80);
            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(p => p.Price)
                .HasColumnName("Price")
                .IsRequired();
        }
    }
}
