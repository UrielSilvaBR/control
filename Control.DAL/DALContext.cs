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
        private IRepository<City> _cities;
        private IRepository<State> _states;
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
        private IRepository<Invoice> _invoices;
        private IRepository<InvoiceItem> _invoiceItems;
        private IRepository<InvoiceTax> _invoiceTaxes;
        private IRepository<MessageLog> _messageLog;
        private IRepository<Company> _companies;
        private IRepository<Branch> _branches;
        private IRepository<InvoiceSerie> _invoiceSeries;
        private IRepository<InvoiceRps> _invoiceRps;
        private IRepository<PurchaseOrder> _purchaseOrders;
        private IRepository<PurchaseOrderItem> _purchaseOrderItems;
        private IRepository<PaymentTerm> _paymentTerm;

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

        public IRepository<City> Cities
        {
            get
            {
                if (_cities == null)
                    _cities = new CityRepository(dbContext);
                return _cities;
            }
        }

        public IRepository<State> States
        {
            get
            {
                if (_states == null)
                    _states = new StateRepository(dbContext);
                return _states;
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

        public IRepository<Invoice> Invoices
        {
            get
            {
                if (_invoices == null)
                    _invoices = new InvoiceRepository(dbContext);
                return _invoices;
            }
        }

        public IRepository<InvoiceItem> InvoiceItems
        {
            get
            {
                if (_invoiceItems == null)
                    _invoiceItems = new InvoiceItemRepository(dbContext);
                return _invoiceItems;
            }
        }

        public IRepository<InvoiceTax> InvoiceTaxes
        {
            get
            {
                if (_invoiceTaxes == null)
                    _invoiceTaxes = new InvoiceTaxRepository(dbContext);
                return _invoiceTaxes;
            }
        }

        public IRepository<MessageLog> MessageLog
        {
            get
            {
                if (_messageLog == null)
                    _messageLog = new MessageLogRepository(dbContext);
                return _messageLog;
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                if (_companies == null)
                    _companies = new CompanyRepository(dbContext);
                return _companies;
            }
        }

        public IRepository<Branch> Branches
        {
            get
            {
                if (_branches == null)
                    _branches = new BranchRepository(dbContext);
                return _branches;
            }
        }

        public IRepository<InvoiceSerie> InvoiceSeries
        {
            get
            {
                if (_invoiceSeries == null)
                    _invoiceSeries = new InvoiceSerieRepository(dbContext);
                return _invoiceSeries;
            }
        }

        public IRepository<InvoiceRps> InvoiceRps
        {
            get
            {
                if (_invoiceRps == null)
                    _invoiceRps = new InvoiceRpsRepository(dbContext);
                return _invoiceRps;
            }
        }

        public IRepository<PurchaseOrder> PurchaseOrders
        {
            get
            {
                if (_purchaseOrders == null)
                    _purchaseOrders = new PurchaseOrderRepository(dbContext);
                return _purchaseOrders;
            }
        }

        public IRepository<PurchaseOrderItem> PurchaseOrderItem
        {
            get
            {
                if (_purchaseOrderItems == null)
                    _purchaseOrderItems = new PurchaseOrderItemRepository(dbContext);
                return _purchaseOrderItems;
            }
        }

        public IRepository<PaymentTerm> PaymentTerms
        {
            get
            {
                if (_paymentTerm == null)
                    _paymentTerm = new PaymentTermRepository(dbContext);
                return _paymentTerm;
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

        public bool Log(int code, string message, string user)
        {
            MessageLog msg = new MessageLog();

            try
            {
                msg.Code = code;
                msg.Description = message;
                msg.MessageDate = DateTime.Now;

                if (!string.IsNullOrEmpty(user))
                    msg.User = user;

                _messageLog.Create(msg);                
                SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (_contacts != null)
                _contacts.Dispose();

            if (_countries != null)
                _countries.Dispose();

            if (_cities != null)
                _cities.Dispose();

            if (_states != null)
                _states.Dispose();

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

            if (_invoices != null)
                _invoices.Dispose();

            if (_invoiceItems != null)
                _invoiceItems.Dispose();

            if (_invoiceTaxes != null)
                _invoiceTaxes.Dispose();

            if (_messageLog != null)
                _messageLog.Dispose();

            if (_companies != null)
                _companies.Dispose();

            if (_branches != null)
                _branches.Dispose();

            if (_invoiceSeries != null)
                _invoiceSeries.Dispose();

            if (_invoiceRps != null)
                _invoiceRps.Dispose();

            if (_purchaseOrders != null)
                _purchaseOrderItems.Dispose();

            if (_purchaseOrderItems != null)
                _purchaseOrderItems.Dispose();

            if (dbContext != null)
                dbContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}