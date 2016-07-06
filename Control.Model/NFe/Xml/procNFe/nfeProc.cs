using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.procNFe
{
    public class nfeProc
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        public NFe NFe { get; set; }

        public protNFe protNFe { get; set; }
    }
}
