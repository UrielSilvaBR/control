using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control.UI.Controllers;
using System.Web.Mvc;
using Control.UI.Tests.Controllers;
using Control.DAL;
using System.Linq;

namespace Control.UI.Tests.Controllers
{
    [TestClass]
    public class EstoqueControllerTest
    {


        private IDALContext context;

        [TestMethod]
        public void MovimentacaoEstoque()
        {
            EstoqueController controller = new EstoqueController();
            //var result = controller.MovimentacaoEstoque(null) as ViewResult;

            //Assert.AreNotEqual(null, result.Produtos);
        }

        [TestMethod]
        public void EstoqueSave_new()
        {
            context = new DALContext();

            EstoqueController controller = new EstoqueController();
            Control.Model.Entities.Storage storage = new Model.Entities.Storage();
            storage.ProductID = context.Products.All().ToList()[0].Id;
            storage.Quantity = 10;
            storage.UpdateDate = DateTime.Now;
            //var result = controller.EstoqueSave(storage) as ViewResult;

            //Assert.AreNotEqual(null, result.Produtos);
        }
    }
}
