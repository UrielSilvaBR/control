using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.retEnviNFe
{
    [XmlRoot("retEnviNFe")]
    public class retEnviNFe
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        public string tpAmb { get; set; }
        public string verAplic { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
        public string cUF { get; set; }
        public DateTime dhRecbto { get; set; }

        public string nRec { get; set; }
        public string tMed { get; set; }

        public protNFe protNFe { get; set; }

        public retEnviNFe()
        {
            protNFe = new protNFe();
        }

    }
}
