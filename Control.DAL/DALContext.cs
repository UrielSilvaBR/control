using Control.DAL.Data;
using Control.DAL.Repository;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL
{
    public class DALContext : IDALContext
    {
        private ControlContext dbContext;

        private IRepository<Contact> _contacts;
        private IRepository<Country> _countries;
        private IRepository<Customer> _customers;
        private IRepository<Order> _orders;
        private IRepository<OrderProduct> _ordersProducts;
        private IRepository<OrderType> _ordersTypes;
        private IRepository<Product> _products;
        private IRepository<Provider> _providers;
        private IRepository<Stock> _stocks;
        private IRepository<Storage> _storages;
        private IRepository<TypeUnit> _typesUnities;
        private IRepository<Unit> _unities;
        private IRepository<Vendor> _vendors;
        private IRepository<Transaction> _transactions;

        public DALContext()
        {
            dbContext = new ControlContext();
        }

        public IRepository<Contact> Contacts
        {
            get
            {
                if (_contacts == null)
                    _contacts = new ContactRepository(dbContext);
                return _contacts;
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                if (_countries == null)
                    _countries = new CountryRepository(dbContext);
                return _countries;
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(dbContext);
                return _customers;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrderRepository(dbContext);
                return _orders;
            }
        }

        public IRepository<OrderProduct> OrdersProducts
        {
            get
            {
                if (_ordersProducts == null)
                    _ordersProducts = new OrderProductRepository(dbContext);
                return _ordersProducts;
            }
        }

        public IRepository<OrderType> OrdersTypes
        {
            get
            {
                if (_ordersTypes == null)
                    _ordersTypes = new OrderTypeRepository(dbContext);
                return _ordersTypes;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(dbContext);
                return _products;
            }
        }

        public IRepository<Provider> Providers
        {
            get
            {
                if (_providers == null)
                    _providers = new ProviderRepository(dbContext);
                return _providers;
            }
        }

        public IRepository<Stock> Stocks
        {
            get
            {
                if (_stocks == null)
                    _stocks = new StockRepository(dbContext);
                return _stocks;
            }
        }

        public IRepository<Storage> Storages
        {
            get
            {
                if (_storages == null)
                    _storages = new StorageRepository(dbContext);
                return _storages;
            }
        }

        public IRepository<TypeUnit> TypesUnities
        {
            get
            {
                if (_typesUnities == null)
                    _typesUnities = new TypeUnitRepository(dbContext);
                return _typesUnities;
            }
        }

        public IRepository<Unit> Unities
        {
            get
            {
                if (_unities == null)
                    _unities = new UnitRepository(dbContext);
                return _unities;
            }
        }

        public IRepository<Vendor> Vendors
        {
            get
            {
                if (_vendors == null)
                    _vendors = new VendorRepository(dbContext);
                return _vendors;
            }
        }


        public IRepository<Transaction> Transactions
        {
            get
            {
                if (_transactions == null)
                    _transactions = new TransactionRepository(dbContext);
                return _transactions;
            }
        }

        public int SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (_contacts != null)
                _contacts.Dispose();

            if (_countries != null)
                _countries.Dispose();

            if (_customers != null)
                _customers.Dispose();

            if (_orders != null)
                _orders.Dispose();

            if (_ordersProducts != null)
                _ordersProducts.Dispose();

            if (_ordersTypes != null)
                _ordersTypes.Dispose();

            if (_products != null)
                _products.Dispose();

            if (_providers != null)
                _providers.Dispose();

            if (_stocks != null)
                _stocks.Dispose();

            if (_storages != null)
                _storages.Dispose();

            if (_typesUnities != null)
                _typesUnities.Dispose();

            if (_unities != null)
                _unities.Dispose();

            if (_vendors != null)
                _vendors.Dispose();

            if (_transactions != null)
                _transactions.Dispose();

            if (dbContext != null)
                dbContext.Dispose();

            GC.SuppressFinalize(this);
        }
        
    }
}
