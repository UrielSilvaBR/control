using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.retEnviNFe
{
    public class protNFe
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        public infProt infProt { get; set; }

        public protNFe()
        {
            infProt = new infProt();
        }
    }
}
