using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class ShippingModeRepository : Repository<ShippingMode>
    {
        public ShippingModeRepository(ControlContext context) : base(context) { }

        public ShippingModeRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
