﻿using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class ProviderRepository : Repository<Provider>
    {
        public ProviderRepository(ControlContext context) : base(context) { }

        public ProviderRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
