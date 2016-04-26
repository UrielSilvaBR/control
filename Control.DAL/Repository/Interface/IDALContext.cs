using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL
{
    public interface IDALContext : IUnitOfWork
    {
        IRepository<Contact> Contacts { get; }
        IRepository<Country> Countries { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderProduct> OrdersProducts { get; }

        IRepository<OrderType> OrdersTypes { get; }
        IRepository<Product> Products { get; }
        IRepository<Provider> Providers { get; }
        //IRepository<Roles> Roles { get; }
        IRepository<Stock> Stocks { get; }

        IRepository<Storage> Storages { get; }
        IRepository<TypeUnit> TypesUnities { get; }
        IRepository<Unit> Unities { get; }
        //IRepository<User> Users { get; }
        //IRepository<UserRoles> UsersRoles { get; }
        IRepository<Vendor> Vendors { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<MessageLog> MessageLog { get; }

        bool Log(int code, string message, string user);

    }
}
