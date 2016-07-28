using Control.DAL.Data;
using Control.Model.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{   
    public class UserRoleRepository : Repository<IdentityUserRole>
    {
        public UserRoleRepository(ControlContext context) : base(context) { }

        public UserRoleRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
