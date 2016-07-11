using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    [Serializable]
    [XmlRoot("enviNFe")]
    public class enviNFe
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        public string idLote { get; set; }
        public string indSinc { get; set; }

    }
}
