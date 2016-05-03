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
            return View("Cadastrar", Invoice);
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

            return View("Cadastrar", model);
        }

        [HttpPost]
        public ActionResult Save(Model.Entities.Invoice Invoice)
        {
            var model = new Models.InvoiceViewModel();

            try
            {
                if (ModelState.IsValid)
                {
                    Invoice.Status = (int)Model.Enums.StatusInvoice.Gerada;
                    Invoice.Items = new List<InvoiceItem>();
                    Invoice.Items.Add(new InvoiceItem() { ProductID = 1, QuantityOrder = 1, SequencialItem = 1, TotalPrice = 1400, UnitPrice = 1400 });
                    Invoice.Taxes = new List<InvoiceTax>();
                    Invoice.Taxes.Add(new InvoiceTax() { ValorIss = Invoice.Valor * 0.03M });

                    context = new DALContext();
                    context.Invoices.Create(Invoice);
                    if (context.SaveChanges() > 0)
                    {
                        model.Invoice = Invoice;
                        model.Invoice.CustomerInvoice = model.Customers.Where(p => p.Id == Invoice.CustomerID).FirstOrDefault();
                        return Content(String.Format("Nota Fiscal {0} incluída com Sucesso!", Invoice.Numero));
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return View("Cadastrar", model);
        }

        public JsonResult Teste(Model.Entities.InvoiceItem Item)
        {
            Item = new InvoiceItem();
            Item.Id = 1;
            string itemJson = JsonConvert.SerializeObject(Item);

            return Json(itemJson);
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