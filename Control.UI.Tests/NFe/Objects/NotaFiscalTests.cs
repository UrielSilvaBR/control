using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control.DAL.NFe.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Control.DAL.NFe.Objects.Tests
{
    [TestClass()]
    public class NotaFiscalTests
    {
        [TestMethod()]
        public void GerarDANFETest()
        {
            var objNFe = new Control.DAL.NFe.Objects.NotaFiscal();
            var arquivoXml = new XmlDocument();
            arquivoXml.Load(@"C:\35160602545405000130550040003692521005210470-nfe.xml");
            objNFe.GerarDANFE(arquivoXml.OuterXml);

            Assert.Fail();
        }

        

        [TestMethod()]
        public void ObterNFePorArquivoXmlTest()
        {
            var arquivoXml = new XmlDocument();
            arquivoXml.Load(@"C:\35160602545405000130550040003692521005210470-nfe.xml");

            var objNFe = Utility.Serialization.Deserialize<Model.NFe.Xml.procNFe.nfeProc>(arquivoXml.OuterXml);

            Assert.Inconclusive();
        }
    }
}