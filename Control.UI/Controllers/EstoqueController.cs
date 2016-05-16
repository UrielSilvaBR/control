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
    public class EstoqueController : Controller
    {

        private IDALContext context;

        // GET: Estoque
        public ActionResult ConsultaEstoque()
        {
            context = new DALContext();
            List<Control.Model.Entities.Storage> retorno = new List<Control.Model.Entities.Storage>();
            retorno = context.Storages.All().ToList(); 
            return View(retorno);
        }

        public ActionResult ListarOrdemDeCompra()
        {
            context = new DALContext();
            List<Control.Model.Entities.Order> retorno = new List<Control.Model.Entities.Order>();
            retorno = context.Orders.All().ToList();
            return View(retorno);
        }

        public ActionResult ConferenciaNotaFiscal(int OrderID, int? InvoiceNumber)
        {
            context = new DALContext();
            Order model = new Order();

            try
            {
                model = context.Orders.Find(p => p.Id == OrderID);

                model.InvoiceNumber = InvoiceNumber;
                model.InvoiceStatus = 0;

                if (model.Id > 0)
                {
                    if (InvoiceNumber == null || InvoiceNumber == 0)
                    {
                        context.Orders.Update(model);
                        context.SaveChanges();
                    }

                    List<Control.Model.Entities.OrderProduct> ListaProdutosPedido = context.OrdersProducts.Filter(p => p.OrderId == OrderID).ToList();

                    return View(ListaProdutosPedido);

                }
                else
                {
                    throw new Exception("ID do pedido inválido.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
             
        }
     
        public ActionResult AprovarPedido(int OrderID, List<Control.Model.Entities.OrderProduct> ListaProdutosPedido)
        {
            context = new DALContext();
            Order model = new Order();

            try
            {
                model = context.Orders.Find(p => p.Id == OrderID);

                if (model.InvoiceNumber == null || model.InvoiceNumber == 0)
                {
                    model.InvoiceStatus = 1;
                }
                model.Status = "EmEstoque";

                if (model.Id > 0)
                {
                    context.Orders.Update(model);


                    foreach (OrderProduct produtoPedido in ListaProdutosPedido)
                    {

                        context.OrdersProducts.Update(produtoPedido);

                        Storage EstoqueProduto = context.Storages.Find(p => p.ProductID == produtoPedido.ProductID);
                        if (EstoqueProduto.Id > 0)
                        {
                            EstoqueProduto.Quantity += (int)produtoPedido.QuantityDeliver;
                            context.Storages.Update(EstoqueProduto);
                        }
                        else
                        {
                            EstoqueProduto = new Storage
                            {
                                ProductID = produtoPedido.ProductID,
                                //ProductUnit =produtoPedido.ProductUnit,
                                Quantity = (int)produtoPedido.QuantityOrder,
                                UpdateDate = DateTime.Now,
                                TypeUnitID = produtoPedido.TypeUnitID,
                                //TypeUnit = 

                            };
                            context.Storages.Create(EstoqueProduto);
                        }
                    }
                    context.SaveChanges();
                    return RedirectToAction("ConsultaEstoque");
                }
                else
                {
                    throw new Exception("ID do pedido inválido.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult MovimentacaoEstoque(int? StorageID)
        {   
            Storage model = new Storage();
            context = new DALContext();


            var ListaProdutos = context.Products.All().ToList();
            ViewBag.ListaProdutos = ListaProdutos;


            var listaUnidades = context.Unities.All().ToList();
            ViewBag.ListaUnidades = listaUnidades;

            var listaEstoque = context.Storages.All().ToList();
            ViewBag.ListaEstoque = listaEstoque;

            if (StorageID > 0)
            {
                model = context.Storages.Find(p => p.Id == StorageID);
            }
             
            return View(model); 
        }

        public ActionResult EstoqueSave(Storage estoque) {

            context = new DALContext();
            estoque.UpdateDate = DateTime.Now;

            if (estoque.Id > 0)
            {
                context.Storages.Update(estoque);
            }
            else
            { 

                context.Storages.Create(estoque);
            }
            context.SaveChanges();
            return RedirectToAction("ConsultaEstoque");
        }

        public ActionResult EstoqueCreate(int? StorageID)
        {
            Storage model = new Storage();
            context = new DALContext();
            

            var ListaProdutos = context.Products.All().ToList();
            ViewBag.ListaProdutos = ListaProdutos;


            var listaUnidades = context.Unities.All().ToList();
            ViewBag.listaUnidades = listaUnidades;

            if (StorageID > 0)
            {
                model = context.Storages.Find(p => p.Id == StorageID);
             
            }  


            return View(model);
        }

    }
}