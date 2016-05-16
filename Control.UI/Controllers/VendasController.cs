using Control.DAL;
using Control.Model.Entities;
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
        #region Metodos obsoletos Pedido

        // GET: Vendas
        //public ActionResult Pedidos()
        //{
        //    PedidoViewModel model = new PedidoViewModel();
        //    return View(model);
        //}

        //public ActionResult PedidosCliente(int ClienteID)
        //{
        //    PedidoViewModel model = new PedidoViewModel();

        //    model.Pedido.CustomerID = ClienteID;
            
        //    return View("Pedidos", model);
        //}

        //public ActionResult SalvarPedido(PedidoViewModel model)
        //{
        //    if (model.SalvarPedido())
        //    {
        //        model.SalvarItens();
        //    }

        //    return View("Invoice", model);
        //}

        //public ActionResult AddItemPedido(PedidoViewModel par)
        //{
        //    if (par.ItemPedido.Id > 0)
        //    {//Update Item
        //        par.ItensPedido.Remove(par.ItensPedido.SingleOrDefault(p => p.Id == par.ItemPedido.Id));
        //        par.ItensPedido.Add(par.ItemPedido);
        //    }
        //    else
        //    {//Insert Item
        //        par.ItensPedido.Add(par.ItemPedido);
        //    }

        //    return View("Pedidos", par);
        //}

        //public ActionResult ListaPedidos(string nomeCliente)
        //{
        //    DALContext context = new DALContext();
        //    List<Order> retorno = new List<Order>();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(nomeCliente))
        //        {
        //            retorno = context.Orders.All().OrderBy(p => p.InsertDate).ToList();
        //        }
        //        else
        //        {
        //            //retorno = context.Orders.All().Where(p => p.CompanyName.ToUpper().Contains(nomeCliente.ToUpper())).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return View(retorno);
        //}

        #endregion


    }
}