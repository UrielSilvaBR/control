﻿using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class InvoiceSerieRepository : Repository<InvoiceSerie>
    {
        public InvoiceSerieRepository(ControlContext context) : base(context) { }

        public InvoiceSerieRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
