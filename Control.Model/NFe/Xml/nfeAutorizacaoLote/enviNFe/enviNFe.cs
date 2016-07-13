using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.enviNFe
{
    [Serializable]
    [XmlRoot("enviNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class enviNFe
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        public string idLote { get; set; }
        public string indSinc { get; set; }

        [XmlElement("NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public NFe NFe { get; set; }

        public enviNFe()
        {
            NFe = new NFe();
        }
    }
}
