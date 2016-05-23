using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class PurchaseOrderItemRepository : Repository<PurchaseOrderItem>
    {

        public PurchaseOrderItemRepository(ControlContext context) : base(context) { }

        public PurchaseOrderItemRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
