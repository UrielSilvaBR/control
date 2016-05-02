using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class BranchRepository : Repository<Branch>
    {
        public BranchRepository(ControlContext context) : base(context) { }

        public BranchRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
