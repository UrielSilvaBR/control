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
        //listas preenchimento
        public List<Product> Produtos { get; set; }
        public List<Customer> Clientes { get; set; }

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

        public ActionResult Cadastrar(Control.UI.Models.InvoiceViewModel Invoice)
        {
            Invoice.Invoice = new Model.Entities.Invoice();
            Invoice.InvoiceItems = new List<InvoiceItem>();
            CarregarListas();
            Invoice.Clientes = Clientes;
            Invoice.Produtos = Produtos;

            return View(Invoice);
        }

        public ActionResult Salvar(Control.UI.Models.InvoiceViewModel Invoice)
        {
            if(ModelState.IsValid)
            {
                if (Invoice.SalvarInvoice())
                    Invoice.SalvarItens();
            }

            return View();
        }

        private void CarregarListas()
        {
            context = new DALContext();
            Produtos = context.Products.All().ToList();
            Clientes = context.Customers.All().ToList();
        }

        public ActionResult GerarNotaFiscal(int InvoiceID)
        {


            return View();
        }
    }
}