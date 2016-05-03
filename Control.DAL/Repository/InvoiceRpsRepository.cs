using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class InvoiceRpsRepository : Repository<InvoiceRps>
    {
        public InvoiceRpsRepository(ControlContext context) : base(context) { }

        public InvoiceRpsRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
