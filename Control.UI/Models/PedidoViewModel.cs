using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control.UI.Models
{
    public class PedidoViewModel
    {
        private IDALContext context;
        //Propriedades Cadastro
        public Order Pedido { get; set; }
        public List<OrderProduct> ItensPedido { get; set; }

        //listas preenchimento
        public List<Product> Produtos { get; set; }
        public List<Customer> Clientes { get; set; }
        public List<Vendor> Vendedores { get; set; }
        public List<Storage> Estoque { get; set; }

        public PedidoViewModel()
        {
            context = new DALContext();
            CarregarListas();
        }

        public PedidoViewModel(int IdPedido)
        {
            context = new DALContext();
            CarregarListas();
        }

        private void CarregarListas()
        {
            Produtos = context.Products.All().ToList();
            Clientes = context.Customers.All().ToList();
            Vendedores = context.Vendors.All().ToList();
            Estoque = context.Storages.All().ToList();
        }
    }
}