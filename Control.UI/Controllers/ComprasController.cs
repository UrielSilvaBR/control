using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Control.DAL.Data;
using Control.Model.Entities;
using Control.DAL;
using Newtonsoft.Json.Linq;

namespace Control.UI.Controllers
{
    public class ComprasController : Controller
    {
        private IDALContext context;

        #region Recebimento e conferencia

        public ActionResult Recebimento()
        {
            context = new DALContext();
            var Orders = context.PurchaseOrders.Filter(p => p.Status == "PEDIDO_PENDENTE");

            return View(Orders);
        }

        [HttpGet]
        public ActionResult Conferencia(int OrderID)
        {
            var model = new Control.UI.Models.PedidoCompraViewModel();
            context = new DALContext();
            PurchaseOrder retorno = new PurchaseOrder();
            try
            {
                retorno = context.PurchaseOrders.Find(p => p.Id == OrderID);
                model.PurchaseOrder = retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        public PartialViewResult ConferenciaGetListaProdtos(int OrderID)
        {
            context = new DALContext();
            var OrderProducts = context.PurchaseOrderItem.Filter(p => p.PurchaseOrderId == OrderID).ToList();
            return PartialView("_ConferenciaListaProdutos", OrderProducts);
        }


        [HttpPost]
        public ActionResult Save(Models.PedidoCompraViewModel Pedido, string itensPedido)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    context = new DALContext();

                    if (Pedido.PurchaseOrder.Id > 0)
                    {
                        Pedido.PurchaseOrder.Status = "PEDIDO_ENTREGUE";
                        context.PurchaseOrders.Update(Pedido.PurchaseOrder);
                    } 

                    if (context.SaveChanges() > 0)
                    {

                        var arrayItensPedido = JArray.Parse(itensPedido);
                        Pedido.PurchaseOrder.Items = ((JArray)arrayItensPedido).Select(x => new Model.Entities.PurchaseOrderItem
                        {
                            Id = (int)x["Id"],
                            PurchaseOrderId = (int)x["IdPedido"],
                            SequencialItem = (int)x["SequencialItem"],
                            QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()), 
                            QuantityDeliver = Convert.ToDecimal(x["QuantityDeliver"].ToString()),
                            ProductID = (int)x["ProductID"],
                            UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                            ItemDiscount = Convert.ToDecimal(x["ItemDiscount"].ToString()),
                            TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString())
                        }).ToList();


                        foreach (var item in Pedido.PurchaseOrder.Items.Where(p => p.Id > 0))
                            context.PurchaseOrderItem.Update(item);
                        
                        context.SaveChanges();

                    }
                }
                else
                {
                    return View("Recebimento", Pedido);
                }

                return View("Create", Pedido);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion


        #region Index

        public ActionResult Index()
        {
            context = new DALContext();
            var Orders = context.PurchaseOrders.Filter(p => p.Status == "PEDIDO_ABERTO");

            return View(Orders);
        }

        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create(Models.PedidoCompraViewModel Pedido)
        {
            return View(Pedido);
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int OrderID)
        {
            var model = new Control.UI.Models.PedidoCompraViewModel();
            context = new DALContext();
            PurchaseOrder retorno = new PurchaseOrder();
            try
            {
                retorno = context.PurchaseOrders.Find(p => p.Id == OrderID);
                model.PurchaseOrder = retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Create", model);
        }

        #endregion

        #region Delete

        [HttpGet]
        public ActionResult Delete(int OrderID)
        {
            context = new DALContext();
            //context.PurchaseOrders.Delete(p => p.Id == OrderID);

            PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(p => p.Id == OrderID);
            purchaseOrder.Status = "CANCELADO";
            context.PurchaseOrders.Update(purchaseOrder);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePurchaseOrderItem(int PurchaseOrderItemID)
        {
            context = new DALContext();
            context.PurchaseOrderItem.Delete(p => p.Id == PurchaseOrderItemID);
            context.SaveChanges();

            return Content("Item Excluído com Sucesso!");
        }

        #endregion

        #region Save

        [HttpPost]
        public ActionResult Save(Models.PedidoCompraViewModel Pedido, string itensPedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    context = new DALContext(); 

                    if (Pedido.PurchaseOrder.Id > 0)
                        context.PurchaseOrders.Update(Pedido.PurchaseOrder);
                    else
                    {
                        Pedido.PurchaseOrder.InsertDate = DateTime.Now;
                        Pedido.PurchaseOrder.Status = "PEDIDO_ABERTO";
                        context.PurchaseOrders.Create(Pedido.PurchaseOrder);
                    }
                    
                    if (context.SaveChanges() > 0)
                    {
                    
                        var arrayItensPedido = JArray.Parse(itensPedido);
                        Pedido.PurchaseOrder.Items = ((JArray)arrayItensPedido).Select(x => new Model.Entities.PurchaseOrderItem
                        {
                            Id = (int)x["Id"],
                            PurchaseOrderId = (int)x["IdPedido"],
                            SequencialItem = (int)x["SequencialItem"],
                            QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                            ProductID = (int)x["ProductID"],
                            UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                            ItemDiscount = Convert.ToDecimal(x["ItemDiscount"].ToString()),
                            TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString())
                        }).ToList();


                        foreach (var item in Pedido.PurchaseOrder.Items.Where(p => p.Id > 0))
                            context.PurchaseOrderItem.Update(item);

                        foreach (var item in Pedido.PurchaseOrder.Items.Where(p => p.Id == 0))
                        {
                            item.PurchaseOrderId = Pedido.PurchaseOrder.Id;
                            context.PurchaseOrderItem.Create(item);
                        }

                        context.SaveChanges();
                        
                    }
                }
                else
                {
                    return View("Create", Pedido);
                }

                return View("Create", Pedido);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        #endregion


        [HttpGet]
        public ActionResult EnviarParaFonecedor(int OrderID)
        {
            context = new DALContext();
            
            PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(p => p.Id == OrderID);
            purchaseOrder.Status = "PEDIDO_PENDENTE";
            context.PurchaseOrders.Update(purchaseOrder);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult GetProducts(int ProductID)
        {
            context = new DALContext();
            var objProduct = context.Products.Find(p => p.Id == ProductID);
            return Json(objProduct);
        }

        public PartialViewResult GetOrderProducts(int OrderID)
        {
            context = new DALContext();
            var OrderProducts = context.PurchaseOrderItem.Filter(p => p.PurchaseOrderId == OrderID).ToList();
            return PartialView("_ListPurchaseOrdemItem", OrderProducts);
        }

        public ActionResult VisualizarProposta(int OrderID)
        {
            context = new DALContext();
            PurchaseOrder retorno = new PurchaseOrder();
            try
            {
                retorno = context.PurchaseOrders.Find(p => p.Id == OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }


    }
}
