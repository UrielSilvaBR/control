using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Models
{
    public class InvoiceViewModel
    {
        private IDALContext context;
        //Propriedades Cadastro
        public Invoice Invoice { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public InvoiceItem InvoiceItem { get; set; }
        public List<Product> Produtos { get; set; }
        public List<Customer> Clientes { get; set; }


        public InvoiceViewModel()
        {
            //context = new DALContext();
            //CarregarListas();
            Invoice = new Invoice();
            //InvoiceItems = new List<InvoiceItem>();
        }

        public InvoiceViewModel(int IdInvoice)
        {
            context = new DALContext();
            CarregarListas();
            Invoice = context.Invoices.Find(p => p.Id == IdInvoice);
            InvoiceItems = context.InvoiceItems.All().Where(p => p.InvoiceId == IdInvoice).ToList();
        }

        private void CarregarListas()
        {
            Produtos = context.Products.All().ToList();
            Clientes = context.Customers.All().ToList();
        }

        public bool SalvarInvoice()
        {
            try
            {
                if (Invoice.Id > 0)
                    context.Invoices.Update(Invoice);
                else
                    context.Invoices.Create(Invoice);

                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SalvarItens()
        {
            if (Invoice.Id <= 0)
                return false;

            try
            {
                int sequencial = 1;
                foreach (InvoiceItem item in InvoiceItems.OrderBy(p => p.SequencialItem))
                {
                    item.SequencialItem = sequencial;
                    item.InvoiceId = Invoice.Id;

                    if (item.Id > 0)
                        context.InvoiceItems.Update(item);
                    else
                        context.InvoiceItems.Create(item);

                    context.SaveChanges();

                    sequencial++;
                }
            }
            catch (Exception ex)
            {
                context.Log(9, ex.Message, null);
                return false;
            }
            return true;
        }
    }
}