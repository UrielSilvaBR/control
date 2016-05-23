using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Models
{
    public class PedidoCompraViewModel
    {
        private IDALContext context;

        public Model.Entities.PurchaseOrder PurchaseOrder { get; set; }
        public List<Model.Entities.Provider> Providers { get; set; }
        public List<Model.Entities.Product> Products { get; set; }
        public Model.Entities.OrderProduct OrderProduct { get; set; }
        public List<SelectListItem> Status { get; set; }

        public PedidoCompraViewModel()
        {
            context = new DALContext();
            Providers = context.Providers.All().OrderBy(p => p.ShortName).ToList();
            Products = context.Products.All().OrderBy(p => p.Name).ToList();
            Products.Insert(0, new Product() { Id = 0, Name = "SELECIONE..." });
            if (PurchaseOrder == null)
            {
                PurchaseOrder = new PurchaseOrder();
                PurchaseOrder.Items = new List<PurchaseOrderItem>();
            }

            #region Metodos Temporarios (CFOP / Status)

      
            #region Status

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

            #endregion

            #endregion

        }
    }
}