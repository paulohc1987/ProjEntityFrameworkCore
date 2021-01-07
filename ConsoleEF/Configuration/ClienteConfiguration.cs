using ConsoleEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEF.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("Varchar(80)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("Char(11)").IsRequired();
            builder.Property(x => x.CEP).HasColumnType("Char(8)").IsRequired();
            builder.Property(x => x.Estado).HasColumnType("Char(2)").IsRequired();
            builder.Property(x => x.Cidade).HasMaxLength(60).IsRequired();
            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}
