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