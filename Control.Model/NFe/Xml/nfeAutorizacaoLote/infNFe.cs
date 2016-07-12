using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class infNFe
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        [XmlAttribute("Id")]
        public string Id { get; set; }

        public ide ide { get; set; }
        public emit emit { get; set; }
        public dest dest { get; set; }

        [XmlElement("det")]
        public det[] det { get; set; }

        public total total { get; set; }
        public transp transp { get; set; }
        public pag pag { get; set; }
        public infAdic infAdic { get; set; }

        public infNFe()
        {
            ide = new ide();
            emit = new emit();
            dest = new dest();
            total = new total();
            transp = new transp();
            pag = new pag();
            infAdic = new infAdic();
        }
    }
}