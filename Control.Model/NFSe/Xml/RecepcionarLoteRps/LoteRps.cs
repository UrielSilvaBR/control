using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    public class LoteRps
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("NumeroLote", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public string NumeroLote { get; set; }

        [XmlElement("Cnpj", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public string Cnpj { get; set; }

        [XmlElement("InscricaoMunicipal", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public string InscricaoMunicipal { get; set; }

        [XmlElement("QuantidadeRps", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public string QuantidadeRps { get; set; }

        [XmlElement("ListaRps", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public ListaRps ListaRps { get; set; }

        public LoteRps()
        {
            ListaRps = new ListaRps();
        }
    }
}
