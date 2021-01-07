using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleEF.Data;
using ConsoleEF.Domain;
using ConsoleEF.Enums;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEF
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new ApplicationContext())
            //{
            //    var existe = db.Database.GetPendingMigrations().Any();

            //    if (existe)
            //    {
            //        // Existe migrações pendentes
            //    }

            //}

            // Descomente a operação que deseje testar 

            //InserirProduto();
            //InserirClientesEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            //RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using (var db = new ApplicationContext())
            {
                // remover cliente desconectado
                //var clienteDes = new Cliente { Id = 2 };
                // ou do banco do Dados
                var cliente = db.Clientes.Find(2);
                //db.Clientes.Remove(cliente);
                //db.Remove(cliente);

                db.Entry(cliente).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        private static void AtualizarDados()
        {
            using (var db = new ApplicationContext())
            {
                //var cliente = db.Clientes.Find(1);
                //cliente.Nome = "Nome Cliente Alterado 01";

                //db.Entry(cliente).State = EntityState.Modified;

                var clienteDesconectado = new
                {
                    Nome = "Cliente desconectado 01",
                    Telefone = "44564456"
                };

                var cliente = new Cliente
                {
                    Id = 1
                };

                db.Attach(cliente);

                db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
                //db.Clientes.Update(cliente);
                db.SaveChanges();
            }
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using (var db = new ApplicationContext())
            {
                var pedidos = db.Pedidos.Include(p => p.Itens).ThenInclude(p => p.Produto).ToList();
                Console.WriteLine(pedidos.Count);
            }
        }


        private static void CadastrarPedido()
        {
            using (var db = new ApplicationContext())
            {
                var cliente = db.Clientes.FirstOrDefault();
                var produto = db.Produtos.FirstOrDefault();

                var pedido = new Pedido
                {
                    ClienteId = cliente.Id,
                    IniciadoEm = DateTime.Now,
                    FinalizadoEm = DateTime.Now,
                    Observacao = "Pedido teste",
                    Status = StatusPedido.Analise,
                    TipoFrete = TipoFrete.SemFrete,
                    Itens = new List<PedidoItem>
                  {
                      new PedidoItem
                      {
                          ProdutoId = produto.Id,
                          Desconto =0,
                          Quantidade = 1,
                          Valor =10,
                      }
                  }

                };

                db.Pedidos.Add(pedido);
                db.SaveChanges();


            }
        }


        private static void InserirProduto()
        {
            var produto = new Produto
            {
                Descricao = "Produto 1",
                CodigoBarras = "121156445",
                Valor = 10m,
                TipoProduto = TipoProduto.Embalagem,
                Ativo = true
            };

            using (var db = new ApplicationContext())
            {
                db.Produtos.Add(produto);
                //db.Set<Produto>().Add(produto);
                //db.Entry(produto).State = EntityState.Added;

                var registros = db.SaveChanges();
                Console.WriteLine("Foi Adicionado {0} produto no Banco de Dados ", registros);
            }
        }

        private static void ConsultarDados()
        {
            using (var db = new ApplicationContext())
            {
                //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
                var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList();
            }
        }

        private static void InserirClientesEmMassa()
        {
            //var produto = new Produto
            //{
            //    Descricao = "Produto 2 em massa",
            //    CodigoBarras = "121156445",
            //    Valor = 10m,
            //    TipoProduto = TipoProduto.Embalagem,
            //    Ativo = true
            //};

            //var cliente = new Cliente
            //{
            //    Nome = "Paulo H",
            //    CEP = "0000000",
            //    Cidade = "Sao Paulo",
            //    Estado = "SP",
            //    Telefone = "9201700000"
            //};

            var listaClientes = new[]
            {
                new Cliente
                {
               Nome = "Paulo H",
               CEP = "0000000",
               Cidade = "Sao Paulo",
               Estado = "SP",
               Telefone = "9201700000"
                },
                new Cliente
                {
               Nome = "Higor",
               CEP = "0000000",
               Cidade = "Sao Paulo",
               Estado = "SP",
               Telefone = "9201700000"
                },
            };

            using (var db = new ApplicationContext())
            {
                //db.AddRange(produto, cliente);
                db.AddRange(listaClientes);
                //db.Set<Cliente>().AddRange(listaClientes);

                var registros = db.SaveChanges();
                Console.WriteLine("Foi Adicionado {0} Clientes em massa ", registros);
            }
        }

    }
}
