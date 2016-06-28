using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using urlWsNFSe = Control.DAL.br.com.ginfes.homologacao;

namespace Control.DAL.NFSe.Objects
{
    public class NotaFiscal
    {
        private const string HEADER = "<ns2:cabecalho versao='3' xmlns:ns2='http://www.ginfes.com.br/cabecalho_v03.xsd'><versaoDados>3</versaoDados></ns2:cabecalho>";
        private urlWsNFSe.ServiceGinfesImplService _wsNFSe = new urlWsNFSe.ServiceGinfesImplService();

        public NotaFiscal()
        {
             _wsNFSe = new br.com.ginfes.homologacao.ServiceGinfesImplService();

            var certificado = Security.Security.ObterCertificadoDigitalPorNome("N C C");

            _wsNFSe.Url = ConfigurationManager.AppSettings["WsGinfes.ServiceGinfesImplService"];
            _wsNFSe.ClientCertificates.Add(certificado);
            _wsNFSe.Timeout = 60000;
        }

        public string GerarNotaFiscal(string arquivoXml)
        {
            return _wsNFSe.RecepcionarLoteRpsV3(HEADER, arquivoXml);
        }

        public void IncluirRPS(Control.Model.Entities.Invoice Invoice)
        {

        }
    }
}