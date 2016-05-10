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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(Models.PedidoViewModel Pedido)
        {
            return View();
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
                    context.Orders.Create(Pedido.Order);

                    if (context.SaveChanges() > 0)
                    {
                        Pedido.Order = Pedido.Order;
                        Pedido.Order.CustomerOrder = Pedido.Customers.Where(p => p.Id == Pedido.Order.CustomerID).FirstOrDefault();
                        return Content(String.Format("<b>Pedido {0}</br> Gerado com Sucesso!</b>;{1}", Pedido.Order.Id, Pedido.Order.Id));
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
            context = new DALContext();
            var objProduct = context.Products.Find(p => p.Id == ProductID);
            return Json(objProduct);
        }
    }
}