using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Controllers
{
    public class CadastroController : Controller
    {
        private IDALContext context;

        #region CLIENTES
        public ActionResult Clientes(string nomeCliente)
        {
            context = new DALContext();
            List<Customer> retorno = new List<Customer>();
            try
            {
                if (string.IsNullOrEmpty(nomeCliente))
                {
                    retorno = context.Customers.All().ToList();
                }
                else
                {
                    retorno = context.Customers.All().Where(p => p.CompanyName.ToUpper().Contains(nomeCliente.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult ClientesEdit(int? ClientID)
        {
            Customer model = new Customer();
            context = new DALContext();

            if (ClientID > 0)
            {
                model = context.Customers.Find(p => p.Id == ClientID);
            }
            return View(model);
        }

        public ActionResult FiltrarCliente(string FiltroCliente)
        {
            return RedirectToAction("Clientes");
        }

        public ActionResult ClientesSave(Customer model)
        {
            context = new DALContext();

            try
            {
                model.RegisterDate = DateTime.Now;
                model.LastUpdate = DateTime.Now;
                model.PhoneCode = 13;
                model.CommercialPolicy = 0;
                model.Discount = 0;                

                if (model.Id > 0)
                {
                    context.Customers.Update(model);
                }
                else
                {
                    context.Customers.Create(model);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Message = "Cliente cadastrado com sucesso.";

            return RedirectToAction("Clientes");
        }

        public ActionResult ClienteDelete(int? ClientID)
        {
            context = new DALContext();

            try
            {
                var retorno = context.Customers.Delete(p => p.Id == ClientID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.SaveChanges();

            ViewBag.Message = "Cliente excluído com sucesso.";

            return RedirectToAction("Clientes");
        }
        #endregion

        #region CONTATOS
        public ActionResult Contatos(string nomeContato)
        {
            context = new DALContext();
            List<Contact> retorno = new List<Contact>();
            try
            {
                if (string.IsNullOrEmpty(nomeContato))
                {
                    retorno = context.Contacts.All().ToList();
                }
                else
                {
                    retorno = context.Contacts.All().Where(p => p.ContatName.ToUpper().Contains(nomeContato.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }
        
        public ActionResult ContatosEdit(int? ContatoID)
        {
            Contact model = new Contact();
            context = new DALContext();

            var listaClientes = context.Customers.All().ToList();
            ViewBag.ListaClientes = listaClientes;

            var listaVendedores = context.Vendors.All().ToList();
            ViewBag.ListaVendedores = listaVendedores;
            

            if (ContatoID > 0)
            {
                model = context.Contacts.Find(p => p.Id == ContatoID);
            }
            return View(model);
        }

        public ActionResult FiltrarContato(string FiltroContato)
        {
            return RedirectToAction("Contatos");
        }

        public ActionResult ContatoSave(Contact model)
        {
            context = new DALContext();

            try
            {
                model.RegisterDate = DateTime.Now;
                model.LastUpdate = DateTime.Now;
                
                if (model.VendorID == null)
                {
                    model.VendorID = 1;
                }

                if (model.Id > 0)
                {
                    context.Contacts.Update(model);
                }
                else
                {
                    context.Contacts.Create(model);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Message = "Contato cadastrado com sucesso.";

            return RedirectToAction("Fornecedores");
        }

        public ActionResult ContatoDelete(int? ContatoID)
        {
            context = new DALContext();

            try
            {
                var retorno = context.Contacts.Delete(p => p.Id == ContatoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.SaveChanges();

            ViewBag.Message = "Contato excluído com sucesso.";

            return RedirectToAction("Fornecedores");
        }
        #endregion

        #region VENDEDORES
        public ActionResult Vendedores(string nomeVendedor)
        {
            context = new DALContext();
            List<Vendor> retorno = new List<Vendor>();
            try
            {
                if (string.IsNullOrEmpty(nomeVendedor))
                {
                    retorno = context.Vendors.All().ToList();
                }
                else
                {
                    retorno = context.Vendors.All().Where(p => p.Name.ToUpper().Contains(nomeVendedor.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult VendedoresEdit(int? VendedorID)
        {
            Vendor model = new Vendor();
            context = new DALContext();

            if (VendedorID > 0)
            {
                model = context.Vendors.Find(p => p.Id == VendedorID);
            }
            return View(model);
        }

        public ActionResult FiltrarVendedores(string FiltroVendedor)
        {
            return RedirectToAction("Vendedores");
        }

        public ActionResult VendedorSave(Vendor model)
        {
            context = new DALContext();

            try
            {
                model.Active = true;

                if (model.PercentCommission <= 0)
                {
                    model.PercentCommission = 5;
                }

                if (model.Id > 0)
                {
                    context.Vendors.Update(model);
                }
                else
                {
                    context.Vendors.Create(model);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Message = "Vendedor cadastrado com sucesso.";

            return RedirectToAction("Vendedores");
        }

        public ActionResult VendedorDelete(int? VendedorID)
        {
            context = new DALContext();

            try
            {
                var retorno = context.Vendors.Delete(p => p.Id == VendedorID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.SaveChanges();

            ViewBag.Message = "Vendedor excluído com sucesso.";

            return RedirectToAction("Vendedores");
        }
        #endregion

        #region FORNECEDORES
        public ActionResult Fornecedores(string nomeFornecedor)
        {
            context = new DALContext();
            List<Provider> retorno = new List<Provider>();
            try
            {
                if (string.IsNullOrEmpty(nomeFornecedor))
                {
                    retorno = context.Providers.All().ToList();
                }
                else
                {
                    retorno = context.Providers.All().Where(p => p.CompanyName.ToUpper().Contains(nomeFornecedor.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult FornecedoresEdit(int? FornecedorID)
        {
            Provider model = new Provider();
            context = new DALContext();

            if (FornecedorID > 0)
            {
                model = context.Providers.Find(p => p.Id == FornecedorID);
            }
            return View(model);
        }

        public ActionResult FiltrarFornecedor(string FiltroFornecedor)
        {
            return RedirectToAction("Fornecedores");
        }

        public ActionResult FornecedorSave(Provider model)
        {
            context = new DALContext();

            try
            {
                model.RegisterDate = DateTime.Now;
                model.LastUpdate = DateTime.Now;
                model.PhoneCode = 13;
                model.CommercialPolicy = 0;
                model.Discount = 0;

                if (model.Id > 0)
                {
                    context.Providers.Update(model);
                }
                else
                {
                    context.Providers.Create(model);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Message = "Fornecedor cadastrado com sucesso.";

            return RedirectToAction("Fornecedores");
        }

        public ActionResult FornecedorDelete(int? FornecedorID)
        {
            context = new DALContext();

            try
            {
                var retorno = context.Providers.Delete(p => p.Id == FornecedorID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.SaveChanges();

            ViewBag.Message = "Fornecedor excluído com sucesso.";

            return RedirectToAction("Fornecedores");
        }
        #endregion

        #region PRODUTOS
        public ActionResult Produtos(string nomeProduto)
        {
            context = new DALContext();
            List<Product> retorno = new List<Product>();
            try
            {
                if (string.IsNullOrEmpty(nomeProduto))
                {
                    retorno = context.Products.All().ToList();
                }
                else
                {
                    retorno = context.Products.All().Where(p => p.Name.ToUpper().Contains(nomeProduto.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult ProdutosEdit(int? ProdutoID)
        {
            Product model = new Product();
            context = new DALContext();

            if (ProdutoID > 0)
            {
                model = context.Products.Find(p => p.Id == ProdutoID);
            }
            return View(model);
        }

        public ActionResult FiltrarProdutos(string FiltroProduto)
        {
            return RedirectToAction("Produtos");
        }

        public ActionResult ProdutosSave(Product prod)
        {
            context = new DALContext();

            try
            {
                prod.AliqICMS = 3;
                prod.CombinedProduct = false;
                prod.MinimumStockAlert = 50;
                prod.TypeUnitID = 1;
                
                if (prod.Id > 0)
                {
                    context.Products.Update(prod);
                }
                else
                {
                    context.Products.Create(prod);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Message = "Produto cadastrado com sucesso.";

            return RedirectToAction("Produtos");
        }

        public ActionResult ProdutoDelete(int? FornecedorID)
        {
            context = new DALContext();

            try
            {
                var retorno = context.Providers.Delete(p => p.Id == FornecedorID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.SaveChanges();

            ViewBag.Message = "Produto excluído com sucesso.";

            return RedirectToAction("Produtos");
        }
        #endregion

        #region CFOP
        #endregion

        //<li>@Html.ActionLink("Clientes", "Clientes", "Cadastro")</li>
        //<li>@Html.ActionLink("Fornecedores", "Fornecedores", "Cadastro")</li>
        //<li>@Html.ActionLink("Produtos", "Produtos", "Cadastro")</li>
        //<li>@Html.ActionLink("Vendedores", "Vendedores", "Cadastro")</li>
        //<li>@Html.ActionLink("Contatos", "Contatos", "Cadastro")</li>
        //<li>@Html.ActionLink("CFOP", "CFOP", "Cadastro")</li>
    }
}