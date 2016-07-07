using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Control.DAL.Data;
using Control.Model.Entities;
using Control.DAL;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml;

namespace Control.UI.Controllers
{
    public class ComprasController : Controller
    {
        private IDALContext context;

        #region Recebimento e conferencia

        public ActionResult Recebimento()
        {
            context = new DALContext();
            var Orders = context.PurchaseOrders.Filter(p => p.Status == "PEDIDO_PENDENTE").ToList();

            return View(Orders);
        }

        [HttpGet]
        public ActionResult Conferencia(int OrderID)
        {
            var model = new Control.UI.Models.PedidoCompraViewModel();
            context = new DALContext();
            try
            {
                var retorno = context.PurchaseOrders.Find(p => p.Id == OrderID);
                model.PurchaseOrder = retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        public PartialViewResult ConferenciaGetListaProdtos(int OrderID)
        {
            context = new DALContext();
            var OrderProducts = context.PurchaseOrderItem.Filter(p => p.PurchaseOrderId == OrderID).ToList();
            return PartialView("_ConferenciaListaProdutos", OrderProducts);
        }


        [HttpPost]
        public ActionResult SaveConferencia(Models.PedidoCompraViewModel Pedido, string itensPedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context = new DALContext();

                    if (Pedido.PurchaseOrder.Id > 0)
                    {
                        Pedido.PurchaseOrder.Status = "PEDIDO_ENTREGUE";
                        Pedido.PurchaseOrder.ProviderID = Pedido.PurchaseOrder.ProviderPurchaseOrder.Id;
                        context.PurchaseOrders.Update(Pedido.PurchaseOrder);
                    }

                    if (context.SaveChanges() > 0)
                    {

                        var arrayItensPedido = JArray.Parse(itensPedido);
                        Pedido.PurchaseOrder.Items = ((JArray)arrayItensPedido).Select(x => new Model.Entities.PurchaseOrderItem
                        {
                            Id = (int)x["Id"],
                            PurchaseOrderId = (int)x["IdPedido"],
                            SequencialItem = (int)x["SequencialItem"],
                            QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                            QuantityDeliver = Convert.ToDecimal(x["QuantityDeliver"].ToString()),
                            ProductID = (int)x["ProductID"],
                            ProductItem = context.Products.Find((int)x["ProductID"]),
                            UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                            ItemDiscount = Convert.ToDecimal(x["ItemDiscount"].ToString()),
                            TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString())
                        }).ToList();


                        foreach (var item in Pedido.PurchaseOrder.Items) {

                            context.PurchaseOrderItem.Update(item); 
                            Storage EstoqueProduto = context.Storages.Find(p => p.ProductID == item.ProductID);
                            if (EstoqueProduto != null)
                            {
                                EstoqueProduto.Quantity += item.QuantityDeliver;
                                context.Storages.Update(EstoqueProduto);
                            }
                            else
                            {
                                EstoqueProduto = new Storage
                                {
                                    ProductID = item.ProductID,
                                    Quantity = item.QuantityDeliver,
                                    UpdateDate = DateTime.Now,

                                };
                                context.Storages.Create(EstoqueProduto);
                            }
                            context.SaveChanges();
                        }

                        Pedido.PurchaseOrder.Items.ForEach(p => 
                        {
                            p.ProductItem.QuantityCurrentStock += p.QuantityDeliver;
                            context.Products.Update(p.ProductItem);
                        });

                        context.SaveChanges();
                    }
                }
                else
                {
                    return Content("Ordem de compra inválida;0");
                }

                return Content("Ordem de compra recebida com<br> Sucesso ! <br> Estoque Atualizado!;" + Pedido.PurchaseOrder.Id.ToString());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion

        #region Index

        public ActionResult Index()
        {
            context = new DALContext();
            var Orders = context.PurchaseOrders.Filter(p => p.Status == "PEDIDO_ABERTO").ToList();

            return View(Orders);
        }

        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create(Models.PedidoCompraViewModel Pedido)
        {
            return View(Pedido);
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int OrderID)
        {
            var model = new Control.UI.Models.PedidoCompraViewModel();
            context = new DALContext();
            
            PurchaseOrder retorno = new PurchaseOrder();
            try
            {
                retorno = context.PurchaseOrders.All().Include("Items").ToList().Find(p => p.Id == OrderID);

                model.PurchaseOrder = retorno;
                model.PurchaseOrder.Items = retorno.Items;

                foreach (var item in model.PurchaseOrder.Items)
                {
                    item.ProductItem = new Product();
                    item.ProductItem = context.Products.Find(x => x.Id == item.ProductID);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Create", model);
        }

        #endregion

        #region Delete

        [HttpGet]
        public ActionResult Delete(int OrderID)
        {
            context = new DALContext();
            //context.PurchaseOrders.Delete(p => p.Id == OrderID);

            PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(p => p.Id == OrderID);
            purchaseOrder.Status = "CANCELADO";
            context.PurchaseOrders.Update(purchaseOrder);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePurchaseOrderItem(int PurchaseOrderItemID)
        {
            context = new DALContext();
            context.PurchaseOrderItem.Delete(p => p.Id == PurchaseOrderItemID);
            context.SaveChanges();

            return Content("Item Excluído com Sucesso!");
        }

        #endregion

        #region Save

        [HttpPost]
        public ActionResult Save(Models.PedidoCompraViewModel Pedido, string itensPedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    context = new DALContext(); 
                    Pedido.PurchaseOrder.InsertDate = DateTime.Now;
                    Pedido.PurchaseOrder.Status = "PEDIDO_ABERTO";

                    if (Pedido.PurchaseOrder.Id > 0)
                        context.PurchaseOrders.Update(Pedido.PurchaseOrder);
                    else
                    {
                        context.PurchaseOrders.Create(Pedido.PurchaseOrder);
                    }
                    
                    if (context.SaveChanges() > 0)
                    {
                    
                        var arrayItensPedido = JArray.Parse(itensPedido);
                        Pedido.PurchaseOrder.Items = ((JArray)arrayItensPedido).Select(x => new Model.Entities.PurchaseOrderItem
                        {
                            Id = (int)x["Id"],
                            PurchaseOrderId = (int)x["IdPedido"],
                            SequencialItem = (int)x["SequencialItem"],
                            QuantityOrder = Convert.ToDecimal(x["QuantityOrder"].ToString()),
                            QuantityDeliver = 0,
                            ProductID = (int)x["ProductID"],
                            UnitPrice = Convert.ToDecimal(x["UnitPrice"].ToString()),
                            ItemDiscount = Convert.ToDecimal(x["ItemDiscount"].ToString()),
                            TotalPrice = Convert.ToDecimal(x["TotalPrice"].ToString())
                        }).ToList();


                        foreach (var item in Pedido.PurchaseOrder.Items.Where(p => p.Id > 0))
                            context.PurchaseOrderItem.Update(item);

                        foreach (var item in Pedido.PurchaseOrder.Items.Where(p => p.Id == 0))
                        {
                            item.PurchaseOrderId = Pedido.PurchaseOrder.Id;
                            context.PurchaseOrderItem.Create(item);
                        }

                        context.SaveChanges();
                        
                    }
                }
                else
                {
                    return Content("Ordem  de Compra inválida;0");
                }

                return Content( "Ordem de Compra salva com <br>Sucesso!;" + Pedido.PurchaseOrder.Id.ToString() );
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        #endregion

        [HttpGet]
        public ActionResult EnviarParaFonecedor(int OrderID)
        {
            context = new DALContext();
            
            PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(p => p.Id == OrderID);
            purchaseOrder.Status = "PEDIDO_PENDENTE";
            context.PurchaseOrders.Update(purchaseOrder);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult GetProducts(int ProductID)
        {
            context = new DALContext();
            var objProduct = context.Products.Find(p => p.Id == ProductID);
            return Json(objProduct);
        }

        public PartialViewResult GetOrderProducts(int OrderID)
        {
            context = new DALContext();
            var OrderProducts = context.PurchaseOrderItem.All().Include("ProductItem").Where(p => p.PurchaseOrderId == OrderID).ToList();
            return PartialView("_ListPurchaseOrdemItem", OrderProducts);
        }

        public ActionResult VisualizarProposta(int OrderID)
        {
            context = new DALContext();
            PurchaseOrder retorno = new PurchaseOrder();
            try
            {
                retorno = context.PurchaseOrders.Find(p => p.Id == OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(retorno);
        }

        [HttpPost]
        public ActionResult UploadXmlNFe()
        {
            HttpPostedFileBase file = null;

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
                    }

                    var arquivoXml = new XmlDocument();
                    arquivoXml.Load(file.InputStream);

                    var objNFe = Utility.Serialization.Deserialize<Model.NFe.Xml.procNFe.nfeProc>(arquivoXml.OuterXml);

                    // Returns message that successfully uploaded  
                    return Json(new { retorno = "Arquivo importado com Sucesso!", nf = objNFe.NFe.infNFe.ide.cNF });
                }
                catch (Exception ex)
                {
                    return Json("Ocorreu um erro. Detalhes do Erro: " + ex.Message);
                }
            }
            else
            {
                return Json("Não existem arquivos selecionados.");
            }
        }
    }
}
