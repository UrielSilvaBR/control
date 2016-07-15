using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.UI.Models
{
    public class ProdutoCarteiraEstoqueViewModel
    {
        public Product Product { get; set; }
        public ProductProvider ProductProvider{get;set;}

        public ProdutoCarteiraEstoqueViewModel()
        {
            if(Product == null)
            {
                Product = new Product();
                ProductProvider = new ProductProvider();
            }
        }
    }
}
