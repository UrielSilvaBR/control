using Control.DAL;
using Control.Model.Entities;
using Control.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Control.UI.Controllers
{
    public class PedidoController : Controller
    {
        private IDALContext context;

        #region Index

        public ActionResult Index()
        {
            context = new DALContext();

            var Orders = context.Orders.All().ToList();
            ViewBag.Title = "Histórico de Pedidos";
            return View(Orders);
        }

        public ActionResult PropostasAbertas()
        {
            context = new DALContext();

            var Orders = context.Orders.All().Where(p => p.Status == "PROPOSTA").ToList();
            ViewBag.Title = "Propostas em Aberto";
            return View(Orders);
        }

        public ActionResult PedidosAbertos()
        {
            context = new DALContext();

            var Orders = context.Orders.All().Where(p => p.Status == "PEDIDO - ABERTO").ToList();
            ViewBag.Title = "Pedidos em Aberto";
            return View("Index", Orders);
        }

        public ActionResult OrdersCustomer(int ClientID)
        {
            context = new DALContext();

            var Orders = context.Orders.Filter(p => p.CustomerID == ClientID).ToList();

            return View("Index", Orders);
        }

        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create(Models.PedidoViewModel Pedido)
        {
            Model.Entities.User usuario = new Model.Entities.User();
            context = new DALContext();

            usuario = context.Users.All().SingleOrDefault(p => p.UserName == User.Identity.Name);

            if (usuario.UserName == "vilo@ncc1701.com.br")
            {
                ViewBag.NomeLogado = "Vilo";
            }
            else
            {
                ViewBag.NomeLogado = "Uriel Silva";
            }

            return View(Pedido);
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int OrderID)
        {
            var model = new Control.UI.Models.PedidoViewModel();
            context = new DALContext();
            Order retorno = new Order();
            try
            {
                retorno = context.Orders.Find(p => p.Id == OrderID);
                model.Order = retorno;
                model.Contacts = context.Contacts.Filter(p => p.CustomerID == model.Order.CustomerID).ToList();
                model.Vendors = context.VendorsCustomer.Filter(p => p.CustomerID == model.Order.CustomerID).Select(p => p.Vendor).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Create", model);
        }

        public ActionResult ConfirmarProposta(int OrderID)
        {
            context = new DALContext();
            Order retorno = new Order();

            try
            {
                retorno = context.Orders.Find(p => p.Id == OrderID);
                retorno.Validated = true;
                context.SaveChanges();

                return Content(String.Format("Proposta  número {0} atualizada com sucesso!", OrderID));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult ConverterPedido(Model.Entities.Order order, int OrderID)
        {
            context = new DALContext();
            Order convertPedido = new Order();

            try
            {
                convertPedido = context.Orders.Find(p => p.Id == OrderID);

                convertPedido.Status = "PEDIDO - ABERTO";
                convertPedido.CustomerControlCode = order.CustomerControlCode;

                context.Orders.Update(convertPedido);
                context.SaveChanges();

                return Content(String.Format("{0}", order.CustomerControlCode));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult ExportarPDF(int OrderID)
        {
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                ViewBag.ToPDF = "1";
                //string path = HttpContext.Server.MapPath("/Invoice/InvoiceFile?InvoiceID=" + OrderID.ToString()); 

                SelectPdf.PdfDocument doc = converter.ConvertUrl("http://control.gtwave.com.br/Invoice/InvoiceFile?InvoiceID=" + OrderID);

                ViewBag.ToPDF = "0";
                //doc.Save(System.Web.HttpContext.Current.Response, false, "test.pdf");
                //doc.Close();
                //doc.Save()

                byte[] fileBytes = doc.Save();
                string fileName = "proposta_" + OrderID.ToString() + ".pdf";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

                //return Content(String.Format("Arquivo exportado.", OrderID));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        #endregion

        #region Delete

        [HttpGet]
        public ActionResult Delete(int OrderID)
        {
            context = new DALContext();
            context.Orders.Delete(p => p.Id == OrderID);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteOrderProduct(int OrderProductID)
        {
            context = new DALContext();
            context.OrdersProducts.Delete(p => p.Id == OrderProductID);
            context.SaveChanges();

            return Content("Item Excluído com Sucesso!");
        }

        #endregion

        #region Save

        [HttpPost]
        public ActionResult Save(Models.PedidoViewModel Pedido, string itensPedido)
        {
            try
            {
                context = new DALContext();

                if (ModelState.IsValid)
                {
                    var arrayItensPedido = JArray.Parse(itensPedido);
                    Pedido.Order.Items = ((JArray)arrayItensPedido).Select(x => new Model.Entities.OrderProduct
                    {
                        Id = (int)x["Id"],
                        OrderId = (int)x["IdPedido"],
                        SequencialItem = (int)x["SequencialItem"],
                        QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                        ProductID = (int)x["ProductID"],
                        ProductItem = context.Products.Find((int)x["ProductID"]),
                        UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                        DeadlineItem = (int)x["DeadlineItem"],
                        ItemDiscount = 0,
                        TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString()),
                        TypeUnitID = 1,
                    }).ToList();

                    Pedido.Order.OrderTypeID = 1;
                    Pedido.Order.Status = "PROPOSTA";

                    Pedido.Order.ShippingId = Pedido.Order.ShippingId.HasValue && Pedido.Order.ShippingId.Value == 0 ? null : Pedido.Order.ShippingId;

                    foreach (var item in Pedido.Order.Items.Where(p => p.OrderId > 0))
                        context.OrdersProducts.Update(item);

                    foreach (var item in Pedido.Order.Items.Where(p => p.OrderId == 0))
                    {
                        item.OrderId = Pedido.Order.Id;
                        context.OrdersProducts.Create(item);
                    }

                    Pedido.Order.ShippingId = null;
                    if (Pedido.Order.Id > 0)
                        context.Orders.Update(Pedido.Order);
                    else
                        context.Orders.Create(Pedido.Order);

                    bool pedidoExiste = Pedido.Order.Id > 0;

                    Pedido.Order.Items.ForEach(p =>
                    {
                        p.ProductItem.QuantityCurrentStock -= p.QuantityOrder;
                        context.Products.Update(p.ProductItem);

                        Storage EstoqueProduto = context.Storages.Find(q => q.ProductID == p.ProductID);
                        if (EstoqueProduto != null)
                        {
                            EstoqueProduto.Quantity -= p.QuantityOrder;
                            context.Storages.Update(EstoqueProduto);
                        }
                        else
                        {
                            EstoqueProduto = new Storage
                            {
                                ProductID = p.ProductID,
                                Quantity = p.QuantityOrder,
                                UpdateDate = DateTime.Now,

                            };
                            context.Storages.Create(EstoqueProduto);
                        }
                    });

                    if (context.SaveChanges() > 0)
                    {
                        Pedido.Order = Pedido.Order;
                        Pedido.Order.CustomerOrder = Pedido.Customers.Where(p => p.Id == Pedido.Order.CustomerID).FirstOrDefault();

                        if (pedidoExiste)
                            return Content(String.Format("<b>Proposta {0}</br> Alterada com Sucesso!</b>;{1}", Pedido.Order.Id, Pedido.Order.Id));
                        else
                            return Content(String.Format("<b>Proposta {0}</br> Incluída com Sucesso!</b>;{1}", Pedido.Order.Id, Pedido.Order.Id));
                    }
                }
                else
                {
                    return View("Create", Pedido);
                }

                return View("Create", Pedido);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        #endregion

        public JsonResult GetProducts(int ProductID)
        {
            try
            {
                context = new DALContext();
                var objProduct = context.Products.Find(p => p.Id == ProductID);

                decimal UnitPrice = objProduct.UnitPrice.HasValue ? objProduct.UnitPrice.Value : 0;
                decimal QuantityCurrentStock = objProduct.QuantityCurrentStock.HasValue ? objProduct.QuantityCurrentStock.Value : 0;

                return Json(new { UnitPrice, QuantityCurrentStock });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public PartialViewResult GetOrderProducts(int OrderID)
        {
            try
            {
                context = new DALContext();
                var OrderProducts = context.OrdersProducts.Filter(p => p.OrderId == OrderID).ToList();
                return PartialView("_ListOrders", OrderProducts);
            }
            catch (Exception ex)
            {
                return PartialView("_ListOrders", ex.Message);
            }
        }

        public JsonResult GetContactsByCustomer(int CustomerID)
        {
            try
            {
                context = new DALContext();
                var contactList = context.Contacts.Filter(p => p.CustomerID == CustomerID).ToList();
                return Json(new { contactList = contactList });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public JsonResult GetVendorsByContact(int ContactID)
        {
            try
            {
                context = new DALContext();
                var vendorList = context.Vendors.Filter(p => p.Id == ContactID).ToList();
                return Json(new { vendorList = vendorList });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public JsonResult GetVendorsByCustomer(int CustomerID)
        {
            try
            {
                context = new DALContext();
                //var vendorList = context.VendorsCustomer.Filter(p => p.CustomerID == CustomerID).Select(p => p.Vendor).ToList();
                var vendorList = new List<Vendor>();

                var Customer = context.Customers.Find(p => p.Id == CustomerID);


                if (Customer != null)
                    vendorList = context.Vendors.Filter(p => p.Id == (Customer.VendorId.HasValue ? Customer.VendorId.Value : 0)).ToList();

                return Json(new { vendorList = vendorList });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public JsonResult GetPaymentTerms(int CustomerID = 0)
        {
            try
            {
                context = new DALContext();
                var paymentTermList = context.PaymentTerms.Filter(p => p.IsActive).OrderBy(p => p.Days).ToList();

                var Customer = new Customer();
                int paymentTermIDCustomer = 0;

                if (CustomerID > 0)
                {
                    Customer = context.Customers.Find(p => p.Id == CustomerID);
                    paymentTermIDCustomer = Customer.PaymentTermId.HasValue ? Customer.PaymentTermId.Value : 0;
                }

                return Json(new { paymentTermList = paymentTermList, paymentTermIDCustomer = paymentTermIDCustomer });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public JsonResult GetShippingModes(int CustomerID = 0)
        {
            try
            {
                context = new DALContext();
                //var shippingModeList = context.ShippingModes.All().OrderBy(p => p.Name).ToList();

                var Customer = new Customer();
                int shippingIDCustomer = 0;

                if (CustomerID > 0)
                {
                    Customer = context.Customers.Find(p => p.Id == CustomerID);
                    shippingIDCustomer = Customer.ShippingId.HasValue ? Customer.ShippingId.Value : 0;
                }

                return Json(new { /*shippingModeList = shippingModeList,*/ shippingIDCustomer = shippingIDCustomer });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult GerarNotaFiscal(int OrderID)
        {
            try
            {
                context = new DALContext();
                var Order = context.Orders.Find(p => p.Id == OrderID);

                int[] idProduto = Order.Items.Select(p => p.ProductID.Value).Distinct().ToArray();

                var ListaProdutos = context.Products.Filter(p => idProduto.Contains(p.Id)).ToList();

                var ListaProdutosOrder = Order.Items
                    .Join(ListaProdutos,
                    Items => Items.ProductID,
                    Prod => Prod.Id,
                    (Items, Prod) => new
                    {
                        QuantidadePedido = Items.QuantityOrder,
                        QuantidadeEstoqueAtual = Prod.QuantityCurrentStock,
                        Produto = Prod
                    }).ToList();

                bool produtoSemEstoque = false;
                var listaProdutosSemEstoque = new List<string>();
                ListaProdutosOrder.ForEach(p =>
                {
                    if (p.QuantidadePedido > p.QuantidadeEstoqueAtual)
                    {
                        produtoSemEstoque = true;
                        listaProdutosSemEstoque.Add(p.Produto.Name);
                    }
                });

                if (produtoSemEstoque)
                    throw new Exception(String.Format("Não foi possível emitir a Nota Fiscal!<br>O Pedido contém Produtos sem Estoque!;{0}", JsonConvert.SerializeObject(listaProdutosSemEstoque.Select(p => p).Distinct().ToList())));

                var invoiceItems = new List<InvoiceItem>();

                Order.Items.ForEach(p =>
                {
                    invoiceItems.Add(new InvoiceItem()
                    {
                        Comments = p.Comments,
                        ItemDiscount = p.ItemDiscount,
                        ProductID = p.ProductID.Value,
                        QuantityDeliver = p.QuantityDeliver.HasValue ? p.QuantityDeliver.Value : 0,
                        QuantityOrder = p.QuantityOrder,
                        SequencialItem = p.SequencialItem,
                        TotalPrice = p.TotalPrice,
                        UnitPrice = p.UnitPrice
                    });
                });

                var invoice = new Invoice();

                invoice.InvoiceSerieID = context.InvoiceSeries.Find(p => p.Descricao == "NFE").Id;
                invoice.Numero = Order.Id;
                invoice.DataEmissao = Order.OrderDate;
                invoice.CustomerID = Order.CustomerID;
                invoice.Status = (int)Model.Enums.StatusInvoice.Gerada;
                invoice.Valor = Order.TotalValue;
                invoice.Items = invoiceItems;

                context.Invoices.Create(invoice);
                context.SaveChanges();

                invoice.Numero = invoice.Id;
                context.Invoices.Update(invoice);
                context.SaveChanges();

                Order.InvoiceNumber = (int)invoice.Numero;
                Order.Status = "PEDIDO - ENTREGUE";
                context.Orders.Update(Order);
                context.SaveChanges();

                return Content(String.Format("Nota Fiscal {0} gerada com sucesso!", invoice.Id));
            }
            catch (Exception ex)
            {
                return Content(String.Format("ERRO;{0}", ex.Message));
            }
        }

        public ActionResult VisualizarProposta(int OrderID)
        {
            context = new DALContext();
            Order retorno = new Order();
            try
            {
                retorno = context.Orders.Find(p => p.Id == OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        public ActionResult IncluirVendedor(Model.Entities.Vendor Vendor, int CustomerID)
        {
            try
            {
                context = new DALContext();

                context.Vendors.Create(Vendor);
                context.SaveChanges();

                context.VendorsCustomer.Create(new VendorsCustomer()
                {
                    CustomerID = CustomerID,
                    VendorID = Vendor.Id
                });

                context.SaveChanges();

                var Customer = context.Customers.Find(p => p.Id == CustomerID);

                Customer.VendorId = Vendor.Id;
                context.Customers.Update(Customer);
                context.SaveChanges();

                return Content(String.Format("{0}", Vendor.Id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult IncluirContato(Model.Entities.Contact Contact, int CustomerID)
        {
            try
            {
                context = new DALContext();

                context.Contacts.Create(Contact);
                context.SaveChanges();

                return Content(String.Format("{0}", Contact.Id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult IncluirCondicaoPagamento(Model.Entities.PaymentTerm PaymentTerm, int CustomerID)
        {
            try
            {
                context = new DALContext();

                context.PaymentTerms.Create(PaymentTerm);

                var Customer = context.Customers.Find(p => p.Id == CustomerID);
                context.SaveChanges();

                Customer.PaymentTermId = PaymentTerm.Id;
                context.Customers.Update(Customer);
                context.SaveChanges();

                return Content(String.Format("{0}", PaymentTerm.Id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult IncluirCliente(Model.Entities.Customer Customer)
        {
            try
            {
                context = new DALContext();

                Customer.Document = Customer.Document.Replace(".", "").Replace("-", "").Replace("/", "");

                context.Customers.Create(Customer);
                context.SaveChanges();

                var CustomerList = context.Customers.All()
                    .Select(p => new
                    {
                        p.Id,
                        p.CompanyName
                    })
                    .OrderBy(p => p.CompanyName).ToList();

                var CustomerListJSON = new JavaScriptSerializer().Serialize(CustomerList);

                return Content(String.Format("{0};{1}", Customer.Id, CustomerListJSON));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Carteira()
        {
            context = new DALContext();
            var Orders = context.Orders.Filter(p => p.Status == "PEDIDO - ABERTO").ToList();

            var OrderProducts = new List<OrderProduct>();

            Orders.ForEach(p =>
            {
                OrderProducts.AddRange(p.Items);
            });

            var CustomerList = OrderProducts.Select(p => p.Order.CustomerOrder).Distinct().ToList();
            var ProductList = OrderProducts.Select(p => p.ProductItem).Distinct().ToList();

            CustomerList.Insert(0, new Customer() { Id = 0, CompanyName = "TODOS" });
            ProductList.Insert(0, new Product() { Id = 0, Name = "TODOS" });

            ViewData["CustomerList"] = CustomerList;
            ViewData["ProductList"] = ProductList;

            return View(OrderProducts);
        }

        public ActionResult CarregarModalEmail(int OrderID)
        {
            try
            {
                context = new DALContext();
                List<UserAdressBook> Contatos = context.AddressBooks.Filter(p => p.Username == User.Identity.Name).ToList();

                HtmlToPdf converter = new HtmlToPdf();
                ViewBag.ToPDF = "1";
                SelectPdf.PdfDocument doc = converter.ConvertUrl("http://control.gtwave.com.br/Invoice/InvoiceFile?InvoiceID=" + OrderID);

                ViewBag.ToPDF = "0";

                byte[] fileBytes = doc.Save();
                string fileName = "proposta_" + OrderID.ToString() + ".pdf";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);



                //return Content(String.Format("Arquivo exportado.", OrderID));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        //public ActionResult EnviarEmailAttached(SendMailViewModel model, int OrderID)
        public ActionResult EnviarEmailAttached(SendMailViewModel model)
        {
            try
            {
                string saudacao = "Bom dia.";

                if (DateTime.Now.Hour >= 12)
                    saudacao = "Boa Tarde.";

                HtmlToPdf converter = new HtmlToPdf();
                ViewBag.ToPDF = "1";
                SelectPdf.PdfDocument doc = converter.ConvertUrl("http://control.gtwave.com.br/Invoice/InvoiceFile?InvoiceID=" + model.Order.Id.ToString());

                //if (model.Order.Id > 0)
                //{
                //    doc = converter.ConvertUrl("http://control.gtwave.com.br/Invoice/InvoiceFile?InvoiceID=" + model.Order.Id);
                //}


                ViewBag.ToPDF = "0";

                byte[] fileBytes = doc.Save();
                string fileName = "proposta_" + model.Order.Id.ToString() + ".pdf";

                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "anexos\\" + fileName, FileMode.OpenOrCreate);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();

                Utilidades.EnvioEmail.EmailHelper.SendMailTemplate(model.Order.ProposalMailList, model.Assunto,
                    saudacao + "<br /><br /> Segue a proposta solicitada. <br /><br />Atenciosamente,", AppDomain.CurrentDomain.BaseDirectory + "anexos\\" + fileName, AppDomain.CurrentDomain.BaseDirectory);
            }
            catch (Exception ex)
            {


                return Content("Não foi possível enviar o email. " + ex.Message);
            }
            return Content("Email Enviado com sucesso.");
        }

        public PartialViewResult GetProdutosCarteira(int CustomerID = 0, int ProductID = 0)
        {
            context = new DALContext();

            var OrderCustomers = context.Orders.Filter(p => p.Status == "PEDIDO - ABERTO").ToList();

            var Orders = OrderCustomers;

            if (CustomerID > 0)
                Orders = Orders.Where(p => p.CustomerID == CustomerID).ToList();

            var OrderProducts = new List<OrderProduct>();

            Orders.ForEach(p =>
            {
                OrderProducts.AddRange(p.Items);
            });

            if (ProductID > 0)
                OrderProducts = OrderProducts.Where(p => p.ProductID == ProductID).ToList();

            var ProductListFilter = OrderProducts.Select(p => new
            {
                Id = p.ProductItem.Id,
                Name = p.ProductItem.Name
            }).Distinct().ToList();

            TempData["ProductListFilter"] = new JavaScriptSerializer().Serialize(ProductListFilter);

            var CustomerListFilter = OrderCustomers.Select(p => new
            {
                Id = p.CustomerOrder.Id,
                CompanyName = p.CustomerOrder.CompanyName
            }).Distinct().ToList();

            TempData["CustomerListFilter"] = new JavaScriptSerializer().Serialize(CustomerListFilter);

            return PartialView("_ListCarteira", OrderProducts);
        }
    }
}