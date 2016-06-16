using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Control.Model.Entities;
using Control.DAL;
using Control.Utility;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Control.UI.Models;

namespace Control.UI.Controllers
{
    public class InvoiceController : Controller
    {
        private IDALContext context;

        public ActionResult Index()
        {
            context = new DALContext();
            List<Invoice> retorno = new List<Invoice>();
            try
            {
                retorno = context.Invoices.All().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult InvoicesCustomer(int ClientID)
        {
            context = new DALContext();
            List<Invoice> retorno = new List<Invoice>();
            try
            {
                retorno = context.Invoices.Filter(p => p.CustomerID == ClientID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Index",retorno);
        }

        public ActionResult Create(Control.UI.Models.InvoiceViewModel Invoice)
        {
            return View("Create", Invoice);
        }

        public JsonResult GetProducts(int ProductID)
        {
            context = new DALContext();
            var objProduct = context.Products.Find(p => p.Id == ProductID);
            return Json(objProduct);
        }

        public ActionResult Edit(int InvoiceID)
        {
            var model = new Control.UI.Models.InvoiceViewModel();
            context = new DALContext();
            Invoice retorno = new Invoice();
            try
            {
                retorno = context.Invoices.Find(p => p.Id == InvoiceID);
                model.Invoice = retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Create", model);
        }

        public ActionResult Invoice(int InvoiceID)
        {
            PedidoViewModel retorno = new PedidoViewModel();

            context = new DALContext();            
            Order pedido = new Order();            
            
            try
            {
                
                pedido = context.Orders.Find(p => p.Id == InvoiceID);
                ViewBag.Cliente = pedido.CustomerOrder.CompanyName;
                ViewBag.DataValidade = pedido.InsertDate.AddDays(15);
                 
                retorno.Order = pedido;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult PDFInvoice(int InvoiceID)
        {
            PedidoViewModel retorno = new PedidoViewModel();

            context = new DALContext();
            Order pedido = new Order();
            try
            {
                pedido = context.Orders.Find(p => p.Id == InvoiceID);
                ViewBag.Cliente = pedido.CustomerOrder.CompanyName;

                retorno.Order = pedido;

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertUrl("http://localhost:13161/Invoice/Invoice?InvoiceID=" + InvoiceID.ToString());

                
                //doc.Save(System.Web.HttpContext.Current.Response, false, "test.pdf");
                //doc.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Invoice", retorno);
        }

        [HttpPost]
        public ActionResult Save(Models.InvoiceViewModel InvoiceModel, string itensNotaFiscal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InvoiceModel.Invoice.Status = (int)Model.Enums.StatusInvoice.Gerada;

                    var arrayItensNotaFiscal = JArray.Parse(itensNotaFiscal);
                    InvoiceModel.Invoice.Items = ((JArray)arrayItensNotaFiscal).Select(x => new Model.Entities.InvoiceItem
                    {
                        Id = (int)x["Id"],
                        SequencialItem = (int)x["SequencialItem"],
                        QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                        ProductID = (int)x["ProductID"],
                        UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                        ItemDiscount = Convert.ToDecimal(x["ItemDiscount"].ToString()),
                        TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString()),
                    }).ToList();

                    context = new DALContext();
                    context.Invoices.Create(InvoiceModel.Invoice);

                    if (context.SaveChanges() > 0)
                    {
                        InvoiceModel.Invoice = InvoiceModel.Invoice;
                        InvoiceModel.Invoice.CustomerInvoice = InvoiceModel.Customers.Where(p => p.Id == InvoiceModel.Invoice.CustomerID).FirstOrDefault();
                        return Content(String.Format("<b>Nota Fiscal {0}</br> Gerada com Sucesso!</b>", InvoiceModel.Invoice.Numero));
                    }
                }
                else
                {
                    return View("Create", InvoiceModel);
                }

                return View("Create", InvoiceModel);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Delete(int InvoiceID)
        {
            context = new DALContext();
            context.Invoices.Delete(p => p.Id == InvoiceID);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GerarArquivoXml(int InvoiceID)
        {
            return Content("É necessário o Certificado Digital para assinar o XML da Nota Fiscal!");
        }
    }
}