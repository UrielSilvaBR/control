using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class CompanyRepository : Repository<Company>
    {
        public CompanyRepository(ControlContext context) : base(context) { }

        public CompanyRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
