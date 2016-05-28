using Control.DAL;
using Control.Model.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Controllers
{
    public class PedidoController : Controller
    {
        private IDALContext context;

        #region Index

        public ActionResult Index()
        {
            context = new DALContext();

            var Orders = context.Orders.All().ToList();

            return View(Orders);
        }

        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create(Models.PedidoViewModel Pedido)
        {
            return View(Pedido);
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int OrderID)
        {
            var model = new Control.UI.Models.PedidoViewModel();
            context = new DALContext();
            Order retorno = new Order();
            try
            {
                retorno = context.Orders.Find(p => p.Id == OrderID);
                model.Order = retorno;
                model.Contacts = context.Contacts.Filter(p => p.CustomerID == model.Order.CustomerID).ToList();
                model.Contacts.Insert(0, new Contact() { Id = 0, ContatName = "SELECIONE..." });
                model.Vendors = context.Vendors.Filter(p => p.Id == model.Order.ContactOrder.VendorID).ToList();
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
            context.Orders.Delete(p => p.Id == OrderID);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteOrderProduct(int OrderProductID)
        {
            context = new DALContext();
            context.OrdersProducts.Delete(p => p.Id == OrderProductID);
            context.SaveChanges();

            return Content("Item Excluído com Sucesso!");
        }

        #endregion

        #region Save

        [HttpPost]
        public ActionResult Save(Models.PedidoViewModel Pedido, string itensPedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var arrayItensPedido = JArray.Parse(itensPedido);
                    Pedido.Order.Items = ((JArray)arrayItensPedido).Select(x => new Model.Entities.OrderProduct
                    {
                        Id = (int)x["Id"],
                        OrderId = (int)x["IdPedido"],
                        SequencialItem = (int)x["SequencialItem"],
                        QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                        ProductID = (int)x["ProductID"],
                        UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                        ItemDiscount = Convert.ToDecimal(x["ItemDiscount"].ToString()),
                        TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString()),
                        TypeUnitID = 1,
                    }).ToList();

                    Pedido.Order.OrderTypeID = 1;

                    context = new DALContext();

                    foreach (var item in Pedido.Order.Items.Where(p => p.OrderId > 0))
                        context.OrdersProducts.Update(item);

                    foreach (var item in Pedido.Order.Items.Where(p => p.OrderId == 0))
                    {
                        item.OrderId = Pedido.Order.Id;
                        context.OrdersProducts.Create(item);
                    }

                    if (Pedido.Order.Id > 0)
                        context.Orders.Update(Pedido.Order);
                    else
                        context.Orders.Create(Pedido.Order);

                    bool pedidoExiste = Pedido.Order.Id > 0;

                    if (context.SaveChanges() > 0)
                    {
                        Pedido.Order = Pedido.Order;
                        Pedido.Order.CustomerOrder = Pedido.Customers.Where(p => p.Id == Pedido.Order.CustomerID).FirstOrDefault();

                        if (pedidoExiste)
                            return Content(String.Format("<b>Proposta {0}</br> Alterada com Sucesso!</b>;{1}", Pedido.Order.Id, Pedido.Order.Id));
                        else
                            return Content(String.Format("<b>Proposta {0}</br> Incluída com Sucesso!</b>;{1}", Pedido.Order.Id, Pedido.Order.Id));
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

        public JsonResult GetProducts(int ProductID)
        {
            try
            {
                context = new DALContext();
                var objProduct = context.Products.Find(p => p.Id == ProductID);
                return Json(objProduct);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public PartialViewResult GetOrderProducts(int OrderID)
        {
            try
            {
                context = new DALContext();
                var OrderProducts = context.OrdersProducts.Filter(p => p.OrderId == OrderID).ToList();
                return PartialView("_ListOrders", OrderProducts);
            }
            catch (Exception ex)
            {
                return PartialView("_ListOrders", ex.Message);
            }
        }

        public JsonResult GetContactsByCustomer(int CustomerID)
        {
            try
            {
                context = new DALContext();
                var contactList = context.Contacts.Filter(p => p.CustomerID == CustomerID).ToList();
                return Json(new { contactList = contactList });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public JsonResult GetVendorsByContact(int ContactID)
        {
            try
            {
                context = new DALContext();
                var vendorList = context.Vendors.Filter(p => p.Id == ContactID).ToList();
                return Json(new { vendorList = vendorList });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult VisualizarProposta(int OrderID)
        {
            context = new DALContext();
            Order retorno = new Order();
            try
            {
                retorno = context.Orders.Find(p => p.Id == OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }
    }
}