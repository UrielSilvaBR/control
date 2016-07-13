using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeStatusServico
{
    [Serializable]
    [XmlRoot("retConsStatServ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retConsStatServ
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }
        
        public int tpAmb { get; set; }
        public string verAplic { get; set; }
        public int cStat { get; set; }
        public string xMotivo { get; set; }
        public int cUF { get; set; }
        public DateTime dhRecbto { get; set; }
        public string tMed { get; set; }
    }
}
