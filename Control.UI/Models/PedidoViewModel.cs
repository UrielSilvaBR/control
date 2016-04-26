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
            Pedido = new Order();
            ItensPedido = new List<OrderProduct>();
        }

        public PedidoViewModel(int IdPedido)
        {
            context = new DALContext();
            CarregarListas();
            Pedido = context.Orders.Find(p => p.Id == IdPedido);
            ItensPedido = context.OrdersProducts.All().Where(p => p.OrderId == IdPedido).ToList();
        }

        private void CarregarListas()
        {
            Produtos = context.Products.All().ToList();
            Clientes = context.Customers.All().ToList();
            Vendedores = context.Vendors.All().ToList();
            Estoque = context.Storages.All().ToList();
        }

        public bool SalvarPedido()
        {
            try
            {
                if (Pedido.Id > 0)                
                    context.Orders.Update(Pedido);                                    
                else                
                    context.Orders.Create(Pedido);

                context.SaveChanges();
                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SalvarItens()
        {
            if (Pedido.Id <= 0)
                return false;

            try
            {
                int sequencial = 1;
                foreach (OrderProduct item in ItensPedido.OrderBy(p => p.SequencialItem))
                {
                    item.SequencialItem = sequencial;
                    item.OrderId = Pedido.Id;

                    if (item.Id > 0)
                        context.OrdersProducts.Update(item);
                    else
                        context.OrdersProducts.Create(item);

                    context.SaveChanges();

                    sequencial++;
                }
            }
            catch (Exception ex)
            {
                context.Log(9, ex.Message, null);
                return false;
            }
            return true;
        }
    }
}