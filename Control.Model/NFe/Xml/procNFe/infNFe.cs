using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.procNFe
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

        //public List<det> det { get; set; }

        public total total { get; set; }
        public transp transp { get; set; }
        public cobr cobr { get; set; }
        public infAdic infAdic { get; set; }

        public infNFe()
        {
            ide = new ide();
            emit = new emit();
            dest = new dest();

            //det = new List<procNFe.det>();

            total = new total();
            transp = new transp();
            cobr = new cobr();
            infAdic = new infAdic();
        }
    }
}
