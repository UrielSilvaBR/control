using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class det
    {
        [XmlAttribute("nItem")]
        public string nItem { get; set; }

        public prod prod { get; set; }
        public imposto imposto { get; set; }

        public det()
        {
            prod = new prod();
            imposto = new imposto();
        }
    }
}
