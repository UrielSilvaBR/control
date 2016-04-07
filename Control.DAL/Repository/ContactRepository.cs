﻿using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class ContactRepository : Repository<Contact>
    {
        public ContactRepository(ControlContext context) : base(context) { }

        public ContactRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
