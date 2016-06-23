using Control.DAL.Data;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository
{
    public class AddressBookRepository : Repository<UserAdressBook>
    {
        public AddressBookRepository(ControlContext context) : base(context) { }

        public AddressBookRepository()
        {
            base.Context = new ControlContext();
        }
    }    
}
