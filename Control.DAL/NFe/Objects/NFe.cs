using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Control.DAL.NFe.Objects
{
    public class NFe
    {
        private DAL.NFe.Proxy.NFeProxy _NFeProxy;

        public NFe()
        {
            _NFeProxy = new Proxy.NFeProxy();
        }

        /// <summary>
        /// Consultar Status do Servico NFe
        /// </summary>
        /// <param name="consStatServ"></param>
        /// <returns></returns>
        public Model.NFe.Xml.nfeStatusServico.retConsStatServ ConsultarStatusServicoNFe(Model.NFe.Xml.nfeStatusServico.consStatServ consStatServ)
        {
            var arquivoXml = Utility.Serialization.Serialize(consStatServ);

            var arquivoXmlNFe = new XmlDocument();
            arquivoXmlNFe.LoadXml(arquivoXml);
            Utility.Utilities.RemoveNamespaceAttributes(arquivoXmlNFe);

            var nfeStatusServico = new DAL.nfe.homologacao.nfestatusservico2.NfeStatusServico2();

            nfeStatusServico.ClientCertificates.Add(_NFeProxy.CertificadoDigital);
            nfeStatusServico.nfeCabecMsgValue = new nfe.homologacao.nfestatusservico2.nfeCabecMsg();

            nfeStatusServico.nfeCabecMsgValue.cUF = _NFeProxy.UF;
            nfeStatusServico.nfeCabecMsgValue.versaoDados = _NFeProxy.Versao;

            var retStatServNFe = nfeStatusServico.nfeStatusServicoNF2(arquivoXmlNFe);

            return Utility.Serialization.Deserialize<Model.NFe.Xml.nfeStatusServico.retConsStatServ>(retStatServNFe.OuterXml);
        }

        /// <summary>
        /// Gerar DANFE a partir do Arquivo Xml da NFe
        /// </summary>
        /// <param name="arquivoXml"></param>
        public void GerarDANFE(string arquivoXml)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://www.webdanfe.com.br/danfe/GeraDanfe.php");

            string postData = String.Format("arquivoXml={0}", arquivoXml);

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);

            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = postBytes.Length;

            Stream requestStream = Request.GetRequestStream();

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)Request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Resposta do Servidor: " + response.StatusCode.ToString());

                var objStream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

                FileStream objWriteStream = new FileStream(@"C:\teste.pdf", FileMode.Create, FileAccess.Write);

                Utility.Utilities.ReadWriteStream(objStream.BaseStream, objWriteStream);
            }
        }
    }
}