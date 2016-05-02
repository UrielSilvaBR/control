using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Control.Model.Entities;
using Control.DAL;

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

        public ActionResult InvoiceCreate(Control.UI.Models.InvoiceViewModel Invoice)
        {
            return View("Cadastrar", Invoice);
        }

        public ActionResult InvoiceEdit(int InvoiceID)
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
        public ActionResult InvoiceSave(Model.Entities.Invoice Invoice)
        {
            var model = new Models.InvoiceViewModel();

            if (ModelState.IsValid)
            {
                Invoice.Status = "GERADA";
                Invoice.Items = new List<InvoiceItem>();
                Invoice.Items.Add(new InvoiceItem() { ProductID = 2, QuantityOrder = 1, SequencialItem = 1, TotalPrice = 1400 });

                context = new DALContext();
                context.Invoices.Create(Invoice);
                if (context.SaveChanges() > 0)
                {
                    model.Invoice = Invoice;
                    model.Invoice.CustomerInvoice = model.Customers.Where(p => p.Id == Invoice.CustomerID).FirstOrDefault();
                    return View("Cadastrar", model);
                }
            }

            return View("Cadastrar", model);
        }

        public ActionResult InvoiceDelete(int InvoiceID)
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