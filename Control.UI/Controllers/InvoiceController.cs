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

        public ActionResult NotasFiscais()
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

        public ActionResult GerarNotaFiscal(int InvoiceID)
        {


            return View();
        }
    }
}