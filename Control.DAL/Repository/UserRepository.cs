using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class UserRepository : Repository<Model.Entities.User>
    {
        public UserRepository(ControlContext context) : base(context) { }

        public UserRepository()
        {
            base.Context = new ControlContext();
        }
    }
}

