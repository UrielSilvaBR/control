using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Utility
{
    [XmlRoot("webservicecep"), Serializable]
    public class CEP
    {
        [XmlElement("resultado")]
        public int IdResultado { get; set; }

        [XmlElement("resultado_txt")]
        public string DescricaoResultado { get; set; }

        [XmlElement("cidade")]
        public string Cidade { get; set; }

        [XmlElement("uf")]
        public string UF { get; set; }

        [XmlElement("bairro")]
        public string Bairro { get; set; }

        [XmlElement("tipo_logradouro")]
        public string TipoLogradouro { get; set; }

        [XmlElement("logradouro")]
        public string Logradouro { get; set; }

        public string LogradouroCompleto
        {
            get
            {
                return String.Format("{0} {1}", TipoLogradouro.ToUpper(), Logradouro.ToUpper());
            }
        }        
    }
}
