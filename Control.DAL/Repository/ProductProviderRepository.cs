using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class ProductProviderRepository : Repository<ProductProvider>
    {
        public ProductProviderRepository(ControlContext context) : base(context) { }

        public ProductProviderRepository()
        {
            base.Context = new ControlContext();
        }
    }
}