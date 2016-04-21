using Control.DAL;
using Control.DAL.Data;
using OFXSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Controllers
{
    public class FinanceiroController : Controller
    {

        private IDALContext context;

        // GET: Financeiro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExtratoBancario()
        {
            context = new DALContext();
            List<Control.Model.Entities.Transaction> retorno = new List<Control.Model.Entities.Transaction>();
            retorno = context.Transactions.All().ToList(); ;

            return View(retorno);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                StreamReader reader = new StreamReader(file.InputStream, Encoding.Default);

                string strOFX = reader.ReadToEnd();

                var parser = new OFXDocumentParser();
                var ofxDocument = parser.Import(strOFX);

                context = new DALContext();

                foreach (OFXSharp.Transaction item in ofxDocument.Transactions)
                {

                    context.Transactions.Create(new Control.Model.Entities.Transaction
                    {
                        TransType = item.TransType.ToString(),
                        Date = item.Date,
                        Amount = item.Amount,
                        TransactionID = item.TransactionID,
                        Name = item.Name,
                        // TransactionInitializationDate = null, // item.TransactionInitializationDate,
                        //FundAvaliabilityDate = null, // item.FundAvaliabilityDate,
                        Memo = item.Memo,
                        IncorrectTransactionID = item.IncorrectTransactionID,
                        TransactionCorrectionAction = item.TransactionCorrectionAction.ToString(),
                        ServerTransactionID = item.ServerTransactionID,
                        CheckNum = item.CheckNum,
                        ReferenceNumber = item.ReferenceNumber,
                        Sic = item.Sic,
                        PayeeID = item.PayeeID,
                        Currency = item.Currency

                    });


                }

                context.SaveChanges();



            }

            return RedirectToAction("ExtratoBancario");
        }

    }
}