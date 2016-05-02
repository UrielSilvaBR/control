using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.Repository.Tests
{
    [TestClass()]
    public class CompanyRepositoryTests
    {
        private IDALContext context;

        [TestMethod()]
        public void CompanyRepositoryTest()
        {

            //context = new DALContext();
            //context.Companies.Create(new Model.Entities.Company() { RazaoSocial = "N.C.C. EQUIPAMENTOS INDUSTRIAIS" });
            //context.SaveChanges();

            //context = new DALContext();
            //context.Branches.Create(new Model.Entities.Branch() { CompanyID = 1, RazaoSocial = "N.C.C. 1701 EQUIPAMENTOS INDUSTRIAIS EIRELI - EPP",
            //    Fantasia = "N.C.C. 1701 EQUIPAMENTOS INDUSTRIAIS",
            //    InscricaoEstadual = 633394857111,
            //    InscricaoMunicipal = 1234556,
            //    CNPJ = 02919862000148
            //});
            //context.SaveChanges();

            //context = new DALContext();
            //context.InvoiceSeries.Create(new Model.Entities.InvoiceSerie() { Descricao = "NFSE" });
            //context.SaveChanges();

            context = new DALContext();
            context.Products.Create(new Model.Entities.Product() { Name = "PLAYSTATION 4", UnitPrice = 1400, Description = "VIDEO GAME", ProductCode = 1, TypeUnitID = 1 });
            context.SaveChanges();

            //context = new DALContext();
            //context.TypesUnities.Create(new Model.Entities.TypeUnit() { Description = "PEÇA", Sign= "PÇ" });
            //context.SaveChanges();


            Assert.Fail();
        }
    }
}