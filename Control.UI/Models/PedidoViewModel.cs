using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public List<Contact> Contatos { get; set; }
        //public List<CFOP> Contatos { get; set; }

        public List<SelectListItem> CFOPs { get; set; }
        public List<SelectListItem> Status { get; set; }

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
            Contatos = context.Contacts.All().ToList();
           
            tempCFOP();
            tempStatus();
            //Add select list item to list of selectlistitems
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

        #region METODOS TEMPORARIOS
        private void tempStatus()
        {
            Status = new List<SelectListItem>();

            SelectListItem stat1 = new SelectListItem() { Value = "ORÇAMENTO", Text = "ORÇAMENTO" };
            SelectListItem stat2 = new SelectListItem() { Value = "PROPOSTA", Text = "PROPOSTA" };
            SelectListItem stat3 = new SelectListItem() { Value = "PEDIDO - ABERTO", Text = "PEDIDO - ABERTO" };
            SelectListItem stat4 = new SelectListItem() { Value = "PEDIDO - PENDENTE", Text = "PEDIDO - PENDENTE" };
            SelectListItem stat5 = new SelectListItem() { Value = "PEDIDO - ENTREGUE", Text = "PEDIDO - ENTREGUE" };
            SelectListItem stat6 = new SelectListItem() { Value = "ENCERRADO", Text = "ENCERRADO" };
            SelectListItem stat7 = new SelectListItem() { Value = "CANCELADO", Text = "CANCELADO" };

            Status.Add(stat1);
            Status.Add(stat2);
            Status.Add(stat3);
            Status.Add(stat4);
            Status.Add(stat5);
            Status.Add(stat6);
            Status.Add(stat7);
        }

        private void tempCFOP()
        {
            CFOPs = new List<SelectListItem>();

            //Create the select list item you want to add
            SelectListItem sli1 = new SelectListItem() { Value = "5101", Text = "VENDA DE PRODUÇÃO DO ESTABELECIMENTO" };
            SelectListItem sli2 = new SelectListItem() { Value = "5102", Text = "VENDA DE MERCADORIA ADQUIR. DE TERCEIROS" };
            SelectListItem sli3 = new SelectListItem() { Value = "6107", Text = "VENDA DE PRODUÇÃO DO ESTABELECIMENTO A NÃO CONTRIBUINTE" };
            SelectListItem sli4 = new SelectListItem() { Value = "6201", Text = "DEVOLUÇÃO DE COMPRA P/INDUSTRIALIZAÇÃO" };
            SelectListItem sli5 = new SelectListItem() { Value = "6202", Text = "DEVOLUÇÃO DE COMPRA P/COMERCIALIZAÇÃO" };

            CFOPs.Add(sli1);
            CFOPs.Add(sli2);
            CFOPs.Add(sli3);
            CFOPs.Add(sli4);
            CFOPs.Add(sli5);
        }
        #endregion
    }
}