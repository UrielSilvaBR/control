using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.procNFe
{
    [Serializable]
    [XmlRoot("nfeProc", Namespace="http://www.portalfiscal.inf.br/nfe")]
    public class nfeProc
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        [XmlElement("NFe", Namespace="http://www.portalfiscal.inf.br/nfe")]
        public NFe NFe { get; set; }

        public protNFe protNFe { get; set; }

        public nfeProc()
        {
            NFe = new NFe();
            protNFe = new protNFe();
        }
    }
}
