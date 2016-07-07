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
        public List<ProductProvider> ProductProviders { get; set; }

        public int ProviderID { get; set; }

        public long  CodeProductProvider
        {
            get
            {
                if (ProductProviders != null)
                {
                    var ProductProvider = ProductProviders.Where(p => p.ProviderID == ProviderID && p.ProductID == Product.Id).FirstOrDefault();
                    return ProductProvider != null && ProductProvider.Code.HasValue ? ProductProvider.Code.Value : 0;
                }
                else
                    return 0;
            }
        }

        public ProdutoViewModel()
        {
            if (ProductProviders == null)
                ProductProviders = new List<ProductProvider>();

            context = new DALContext();



        }
    }
}