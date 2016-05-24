using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class StateRepository : Repository<State>
    {
        public StateRepository(ControlContext context) : base(context) { }

        public StateRepository()
        {
            base.Context = new ControlContext();
        }
    }
}