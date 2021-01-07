using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConsoleEF.Domain;

namespace ConsoleEF.Configuration 
{
    class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CodigoBarras).HasColumnType("Varchar(14)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("Varchar(60)").IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.TipoProduto).HasConversion<string>();
        }
    }
}
