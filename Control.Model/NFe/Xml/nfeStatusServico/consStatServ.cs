using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeStatusServico
{
    [Serializable]
    [XmlRoot("consStatServ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class consStatServ
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        public string tpAmb { get; set; }
        public string cUF { get; set; }
        public string xServ { get; set; }
    }
}