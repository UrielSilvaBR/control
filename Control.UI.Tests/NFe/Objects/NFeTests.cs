using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control.DAL.NFe.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.NFe.Objects.Tests
{
    [TestClass()]
    public class NFeTests
    {
        [TestMethod()]
        public void EmitirNFeTest()
        {
            var objNFe = new Model.NFe.Xml.nfeAutorizacaoLote.enviNFe.enviNFe();

            objNFe.idLote = "0";
            objNFe.indSinc = "0";

            #region Nota Fiscal

            objNFe.NFe.infNFe.ide.cNF = Utility.Utilities.GetRandomNumber(8);
            objNFe.NFe.infNFe.ide.cUF = "35";
            objNFe.NFe.infNFe.ide.dhEmi = DateTime.Now;
            objNFe.NFe.infNFe.ide.nNF = "1";
            objNFe.NFe.infNFe.ide.serie = "4";
            objNFe.NFe.infNFe.ide.tpAmb = "2";
            objNFe.NFe.infNFe.ide.tpNF = "1";
            objNFe.NFe.infNFe.ide.mod = "55";
            objNFe.NFe.infNFe.ide.cMunFG = "3548708";
            objNFe.NFe.infNFe.ide.tpEmis = "1";

            #endregion

            #region Emitente

            objNFe.NFe.infNFe.emit.enderEmit.UF = "35";
            objNFe.NFe.infNFe.emit.CNPJ = "02919862000148";

            #endregion

            #region Destinatario


            #endregion

            #region Itens Nota Fiscal



            #endregion

            #region Total

            #endregion

            #region Transporte


            #endregion

            #region Cobranca


            #endregion

            #region Informacoes Adicionais


            #endregion

            #region Chave NFe

            objNFe.NFe.infNFe.GerarChaveAcessoNFe();

            #endregion

            var objNFeDAL = new DAL.NFe.Objects.NFe();
            objNFeDAL.EmitirNFe(objNFe);

            Assert.Fail();
        }
    }
}