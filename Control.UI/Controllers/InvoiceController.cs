using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult NotasFiscais()
        {
            return View();
        }
    }
}