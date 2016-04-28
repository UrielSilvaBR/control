using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    public class Endereco
    {
        [XmlElement("Endereco")]
        public string DescEndereco { get; set; }

        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

    }
}
