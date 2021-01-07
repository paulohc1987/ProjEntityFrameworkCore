using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ConsoleEF.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEF.Configuration
{
    class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantidade).HasDefaultValue("1").IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Desconto).IsRequired(); 
        }
    }
}
