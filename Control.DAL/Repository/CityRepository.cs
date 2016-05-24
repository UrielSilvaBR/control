using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class CityRepository : Repository<City>
    {
        public CityRepository(ControlContext context) : base(context) { }

        public CityRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
