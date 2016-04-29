using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control.DAL.NFSe.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Control.DAL.NFSe.Objects.Tests
{
    [TestClass()]
    public class NotaFiscalTests
    {
        [TestMethod()]
        public void GerarArquivoXmlModeloTest()
        {
            var EnviarLoteRpsEnvio = new Model.NFSe.Xml.RecepcionarLoteRps.EnviarLoteRpsEnvio();

            #region LoteRps

            EnviarLoteRpsEnvio.LoteRps.Id = "A101";
            EnviarLoteRpsEnvio.LoteRps.NumeroLote = "101";
            EnviarLoteRpsEnvio.LoteRps.Cnpj = "15438788000190";
            EnviarLoteRpsEnvio.LoteRps.InscricaoMunicipal = "1301669";
            EnviarLoteRpsEnvio.LoteRps.QuantidadeRps = "1";

            #endregion

            #region ListaRps

            #region Rps

            #region InfRps

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Id = "A201";

            #region IdentificacaoRps

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.IdentificacaoRps.Numero = "424706";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.IdentificacaoRps.Serie = "NFSE";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.IdentificacaoRps.Tipo = "1";

            #endregion

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.DataEmissao = String.Format("{0:yyyy-MM-dd}T00:00:00", DateTime.Now);
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.NaturezaOperacao = "1";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.OptanteSimplesNacional = "2";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.IncentivadorCultural = "2";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Status = "1";

            #region RpsSubstituido

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.RpsSubstituido.Numero = "";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.RpsSubstituido.Serie = "";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.RpsSubstituido.Tipo = "";

            #endregion

            #region Servico

            #region Valores

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorServicos = "200.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorPis = "0.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorCofins = "0.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorInss = "0.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorIr = "0.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorCsll = "0.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.IssRetido = "2";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorIss = "6.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.BaseCalculo = "200.00";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.Aliquota = "0.03";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Valores.ValorLiquidoNfse = "200.00";

            #endregion

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.ItemListaServico = "2001";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.CodigoTributacaoMunicipio = "523110201";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.Discriminacao = "*Venda de Produto TESTE 1*Venda de Produto TESTE 2";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Servico.CodigoMunicipio = "3548500";

            #endregion

            #region Prestador

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Prestador.Cnpj = "02919862000148";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Prestador.InscricaoMunicipal = "1301669";

            #endregion

            #region Tomador

            #region IdentificacaoTomador

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = "17660594000160";

            #endregion

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.RazaoSocial = "GT Wave Serviços em Tecnologia da Informação";

            #region Endereco

            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.Endereco.DescEndereco = "R Luiz Antonio Burgain";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.Endereco.Numero = "03";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.Endereco.Bairro = "Perus ";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.Endereco.CodigoMunicipio = "3550308";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.Endereco.Uf = "SP";
            EnviarLoteRpsEnvio.LoteRps.ListaRps.Rps.InfRps.Tomador.Endereco.Cep = "05203150";

            #endregion

            #endregion

            #endregion


            #endregion

            #endregion

            var objXml = Utility.Serialization.Serialize(EnviarLoteRpsEnvio);

            var arquivoXml = new XmlDocument();
            arquivoXml.LoadXml(Utility.Utilities.RemoverBookMark(objXml));

            Utility.Utilities.RemoveNamespaceAttributes(arquivoXml);

            var obj = Utility.Serialization.Deserialize<Model.NFSe.Xml.RecepcionarLoteRps.EnviarLoteRpsEnvio>(arquivoXml.OuterXml);

            arquivoXml.InnerXml = Security.Security.AssinaturaDigital(arquivoXml.InnerXml, "InfRps", Security.Security.ObterCertificadoDigitalPorNome("MARIA APARECIDA NASCIMENTO"));

            var objNotaFiscalDAL = new Control.DAL.NFSe.Objects.NotaFiscal();

            var retornoEnvioNFSe = objNotaFiscalDAL.GerarNotaFiscal(arquivoXml.InnerXml);

            Assert.Fail();
        }

        [TestMethod()]
        public void ObterArquivoXmlModeloTest()
        {
            string path = @"C:\Users\Paulo\Desktop\NFSe_NCC_XML.xml";
            var objArquivoXml = new XmlDocument();
            objArquivoXml.Load(path);

            objArquivoXml.InnerXml = Security.Security.AssinaturaDigital(objArquivoXml.OuterXml, "InfRps", Security.Security.ObterCertificadoDigitalPorNome("MARIA APARECIDA NASCIMENTO"));

            var objNotaFiscalDAL = new Control.DAL.NFSe.Objects.NotaFiscal();

            var retornoEnvioNFSe = objNotaFiscalDAL.GerarNotaFiscal(objArquivoXml.InnerXml);

            Assert.Fail();
        }
    }
}