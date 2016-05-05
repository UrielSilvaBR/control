using Control.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Controllers
{
    public class VendasController : Controller
    {
        // GET: Vendas
        public ActionResult Pedidos()
        {
            PedidoViewModel model = new PedidoViewModel();
            return View(model);
        }

        public ActionResult PedidosCliente(int ClienteID)
        {
            PedidoViewModel model = new PedidoViewModel();

            model.Pedido.CustomerID = ClienteID;
            
            return View("Pedidos", model);
        }

        public ActionResult SalvarPedido(PedidoViewModel model)
        {
            if (model.SalvarPedido())
            {
                model.SalvarItens();
            }

            return View("Invoice", model);
        }

        public ActionResult AddItemPedido(PedidoViewModel par)
        {
            if (par.ItemPedido.Id > 0)
            {//Update Item
                par.ItensPedido.Remove(par.ItensPedido.SingleOrDefault(p => p.Id == par.ItemPedido.Id));
                par.ItensPedido.Add(par.ItemPedido);
            }
            else
            {//Insert Item
                par.ItensPedido.Add(par.ItemPedido);
            }

            return View("Pedidos", par);
        }
    }
}