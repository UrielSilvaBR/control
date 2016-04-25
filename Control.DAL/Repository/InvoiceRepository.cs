using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public InvoiceRepository(ControlContext context) : base(context) { }

        public InvoiceRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
