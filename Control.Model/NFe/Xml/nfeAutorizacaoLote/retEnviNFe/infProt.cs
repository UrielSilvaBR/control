using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.retEnviNFe
{
    public class infProt
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        public string tpAmb { get; set; }
        public string verAplic { get; set; }
        public string chNFe { get; set; }
        public DateTime dhRecbto { get; set; }
        public string nProt { get; set; }
        public string digVal { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
    }
}