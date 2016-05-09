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

        public ActionResult Create(Control.UI.Models.InvoiceViewModel Invoice)
        {
            ViewData["Products"] = Invoice.Products;
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

        [HttpPost]
        public ActionResult Save(Models.InvoiceViewModel model, string itensNotaFiscal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Invoice.Status = (int)Model.Enums.StatusInvoice.Gerada;

                    var arrayItensNotaFiscal = JArray.Parse(itensNotaFiscal);
                    model.Invoice.Items = ((JArray)arrayItensNotaFiscal).Select(x => new Model.Entities.InvoiceItem
                    {
                        Id = (int)x["Id"],
                        SequencialItem = (int)x["SequencialItem"],
                        QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                        ProductID = 1
                    }).ToList();

                    context = new DALContext();
                    context.Invoices.Create(model.Invoice);

                    if (context.SaveChanges() > 0)
                    {
                        model.Invoice = model.Invoice;
                        model.Invoice.CustomerInvoice = model.Customers.Where(p => p.Id == model.Invoice.CustomerID).FirstOrDefault();
                        return Content(String.Format("<b>Nota Fiscal {0}</br> Gerada com Sucesso!</b>", model.Invoice.Numero));
                    }
                }
                else
                {
                    return View("Create", model);
                }

                return View("Create", model);
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
        public ActionResult GerarNotaFiscal(int InvoiceID)
        {
            return View("Index");
        }
    }
}