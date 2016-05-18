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
        
        public Invoice Invoice { get; set; }
        public List<Customer> Customers { get; set; }
        public List<InvoiceSerie> Series { get; set; }
        public List<Product> Products { get; set; }
        public InvoiceItem InvoiceItem { get; set; }

        public InvoiceViewModel()
        {
            context = new DALContext();
            Customers = context.Customers.All().OrderBy(p => p.ShortName).ToList();
            Series = context.InvoiceSeries.All().OrderBy(p => p.Descricao).ToList();
            Products = context.Products.All().OrderBy(p => p.Description).ToList();
            Products.Insert(0, new Product() { Id = 0, Name = "SELECIONE..." });
            if (Invoice == null)
            {
                Invoice = new Invoice();
                InvoiceItem = new InvoiceItem();
                Invoice.Items = new List<InvoiceItem>();
            }
            
        }
    }
}