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

        public InvoiceViewModel()
        {
            context = new DALContext();
            Customers = context.Customers.All().ToList();
            Series = context.InvoiceSeries.All().ToList();
            if(Invoice == null)
            {
                Invoice = new Invoice();
                Invoice.Items = new List<InvoiceItem>();
            }
            
        }
    }
}