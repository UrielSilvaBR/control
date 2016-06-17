﻿using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class VendorsCustomerRepository : Repository<VendorsCustomer>
    {
        public VendorsCustomerRepository(ControlContext context) : base(context) { }

        public VendorsCustomerRepository()
        {
            base.Context = new ControlContext();
        }
    }
}