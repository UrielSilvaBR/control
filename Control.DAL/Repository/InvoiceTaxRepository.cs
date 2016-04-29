using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class InvoiceTaxRepository : Repository<InvoiceTax>
    {
        public InvoiceTaxRepository(ControlContext context) : base(context) { }

        public InvoiceTaxRepository()
        {
            base.Context = new ControlContext();
        }
    }
}