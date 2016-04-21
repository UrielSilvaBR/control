using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class TransactionRepository : Repository<Transaction>
    {
        public TransactionRepository(ControlContext context) : base(context) { }

        public TransactionRepository()
        {
            base.Context = new ControlContext();
        }
    }
}
