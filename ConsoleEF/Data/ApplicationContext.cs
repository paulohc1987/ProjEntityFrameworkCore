using System;
using System.Collections.Generic;
using System.Text;
using ConsoleEF.Domain;
using Microsoft.EntityFrameworkCore;
using ConsoleEF.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleEF.Data
{
    public class ApplicationContext : DbContext
    {
        public static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ConsoleEFCore;Integrated Security=true",
                p => p.EnableRetryOnFailure(maxRetryCount: 4, maxRetryDelay:TimeSpan.FromSeconds(5), errorNumbersToAdd: null).MigrationsHistoryTable("Console_ef_core_history"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            //modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            //modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            //modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
