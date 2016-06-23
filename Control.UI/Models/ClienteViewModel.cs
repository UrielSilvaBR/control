using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control.UI.Models
{
    public class ClienteViewModel
    {
        private IDALContext context;

        public Customer Customer { get; set; }
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<State> States { get; set; }
        public List<PaymentTerm> CondicoesPagamento { get; set; }
        public List<ShippingMode> ModolidadeTransporte { get; set; }
        public List<Vendor> Vendedores { get; set; }


        public ClienteViewModel()
        {
            if (Customer == null)
            {
                Customer = new Customer();
            }

            context = new DALContext();
            Countries = context.Countries.All().OrderBy(p => p.Name).ToList();
            Cities = new List<City>();
            States = context.States.All().OrderBy(p => p.Name).ToList();
            States.Insert(0, new State() { Id = 0, Name = "SELECIONE..." });

            CondicoesPagamento = context.PaymentTerms.All().ToList();
            CondicoesPagamento.Insert(0, new PaymentTerm() { Id = 0, Description = "SELECIONE..." });

            ModolidadeTransporte = context.ShippingModes.All().ToList();
            ModolidadeTransporte.Insert(0, new ShippingMode() { Id = 0, Name = "SELECIONE..." });

            Vendedores = context.VendorsCustomer.Filter(p => p.CustomerID == Customer.Id).Select(p => p.Vendor).ToList();
            
        }
    }
}