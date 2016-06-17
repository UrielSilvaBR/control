using Control.DAL;
using Control.Model.Entities;
using Newtonsoft.Json.Linq;
using SelectPdf;
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

        public ActionResult OrdersCustomer(int ClientID)
        {
            context = new DALContext();

            var Orders = context.Orders.Filter(p => p.CustomerID == ClientID).ToList();

            return View("Index", Orders);
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
                model.Vendors = context.VendorsCustomer.Filter(p => p.CustomerID == model.Order.CustomerID).Select(p => p.Vendor).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Create", model);
        }

        public ActionResult ConfirmarProposta(int OrderID)
        {
            context = new DALContext();
            Order retorno = new Order();

            try
            {
                retorno = context.Orders.Find(p => p.Id == OrderID);
                retorno.Validated = true;
                context.SaveChanges();

                return Content(String.Format("Proposta  número {0} atualizada com sucesso!", OrderID));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }     
        }

        public ActionResult ExportarPDF(int OrderID)
        {
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                ViewBag.ToPDF = "1";
                SelectPdf.PdfDocument doc = converter.ConvertUrl("http://localhost:13161/Invoice/Invoice?InvoiceID=" + OrderID);

                ViewBag.ToPDF = "0";
                //doc.Save(System.Web.HttpContext.Current.Response, false, "test.pdf");
                //doc.Close();
                //doc.Save()

                byte[] fileBytes = doc.Save();
                string fileName = "proposta_" + OrderID.ToString() + ".pdf";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

                //return Content(String.Format("Arquivo exportado.", OrderID));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            
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
                        DeadlineItem = (int)x["DeadlineItem"],
                        ItemDiscount = 0,
                        TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString()),
                        TypeUnitID = 1,
                    }).ToList();

                    Pedido.Order.OrderTypeID = 1;
                    Pedido.Order.Status = "PROPOSTA";

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

        public JsonResult GetVendorsByCustomer(int CustomerID)
        {
            try
            {
                context = new DALContext();
                var vendorList = context.VendorsCustomer.Filter(p => p.CustomerID == CustomerID).Select(p => p.Vendor).ToList();
                return Json(new { vendorList = vendorList });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult GerarNotaFiscal(int OrderID)
        {
            try
            {
                context = new DALContext();
                var Order = context.Orders.Find(p => p.Id == OrderID);

                var invoiceItems = new List<InvoiceItem>();

                Order.Items.ForEach(p =>
                {
                    invoiceItems.Add(new InvoiceItem()
                    {
                        Comments = p.Comments,
                        ItemDiscount = p.ItemDiscount,
                        ProductID = p.ProductID.Value,
                        QuantityDeliver = p.QuantityDeliver.HasValue ? p.QuantityDeliver.Value : 0,
                        QuantityOrder = p.QuantityOrder,
                        SequencialItem = p.SequencialItem,
                        TotalPrice = p.TotalPrice,
                        UnitPrice = p.UnitPrice
                    });
                });

                var invoice = new Invoice();
                
                invoice.InvoiceSerieID = context.InvoiceSeries.Find(p => p.Descricao == "NFSE").Id;
                invoice.Numero = Order.Id;
                invoice.DataEmissao = Order.OrderDate;
                invoice.CustomerID = Order.CustomerID;
                invoice.Status = (int)Model.Enums.StatusInvoice.Gerada;
                invoice.Valor = Order.TotalValue;
                invoice.Items = invoiceItems;

                context.Invoices.Create(invoice);
                context.SaveChanges();

                invoice.Numero = invoice.Id;
                context.Invoices.Update(invoice);
                context.SaveChanges();

                Order.InvoiceNumber = (int)invoice.Numero;
                context.Orders.Update(Order);
                context.SaveChanges();

                return Content(String.Format("Nota Fiscal {0} gerada com sucesso!", invoice.Id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
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

        public ActionResult IncluirVendedor(Model.Entities.Vendor Vendor, int CustomerID)
        {
            try
            {
                context = new DALContext();

                context.Vendors.Create(Vendor);
                context.SaveChanges();

                context.VendorsCustomer.Create(new VendorsCustomer()
                {
                    CustomerID = CustomerID,
                    VendorID = Vendor.Id
                });

                context.SaveChanges();

                return Content(String.Format("{0}", Vendor.Id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult IncluirContato(Model.Entities.Contact Contact, int CustomerID)
        {
            try
            {
                context = new DALContext();

                context.Contacts.Create(Contact);
                context.SaveChanges();

                return Content(String.Format("{0}", Contact.Id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

    }
}