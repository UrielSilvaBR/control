using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>
    {

        public PurchaseOrderRepository(ControlContext context) : base(context) { }

        public PurchaseOrderRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
