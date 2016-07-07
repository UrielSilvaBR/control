using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control.UI.Models
{
    public class ProdutoViewModel
    {
        private IDALContext context;

        public Product Product { get; set; }
        public List<Provider> Providers { get; set; }

        public int ProviderID { get; set; }

        public long CodigoProdutoFornecedor { get; set; }

        public void AtualizarCodigoProdutoFornecedor()
        {
            context = new DALContext();
            var providerProduct = context.ProductProviders.Filter(p => p.ProviderID == ProviderID && p.ProductID == Product.Id).FirstOrDefault();

            if (providerProduct == null)
                CodigoProdutoFornecedor = 0;
            else
                CodigoProdutoFornecedor = providerProduct.Code.HasValue ? providerProduct.Code.Value : 0;
        }

        public ProdutoViewModel()
        {
            if (Product == null)
                Product = new Product();

            context = new DALContext();
            Providers = context.Providers.All().OrderBy(p => p.CompanyName).ToList();
        }
    }
}