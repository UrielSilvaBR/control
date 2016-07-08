using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.NFe.Proxy
{
    public class NFeProxy
    {
        private string _versao;
        private string _cUF;

        public NFeProxy()
        {
            _versao = "3.10";
            _cUF = "35";
        }

        public NFeProxy(string versao, string cUF) : this()
        {
            _versao = versao;
            _cUF = cUF;
        }

        public X509Certificate2 CertificadoDigital
        {
            get
            {
                var certificado = Security.Security.ObterCertificadoDigitalPorNome("N C C");

                if (certificado == null)
                    throw new Exception("Não foi possível localizar o Certificado Digital!");
                else
                    return certificado;
            }
        }

        public string Versao
        {
            get
            {
                return _versao;
            }
        }

        public string UF
        {
            get
            {
                return _cUF;
            }
        }
    }
}