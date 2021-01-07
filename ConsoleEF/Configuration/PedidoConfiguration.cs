using System;
using System.Collections.Generic;
using System.Text;
using ConsoleEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEF.Configuration 
{
    class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(x => x.Status).HasConversion<string>();
            builder.Property(x => x.TipoFrete).HasConversion<int>();
            builder.Property(x => x.Observacao).HasColumnType("Varchar(512)");

            builder.HasMany(x => x.Itens).WithOne(x => x.Pedido).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
