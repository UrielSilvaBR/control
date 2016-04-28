using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    public class InfRps
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        public IdentificacaoRps IdentificacaoRps { get; set; }
        public string DataEmissao { get; set; }
        public string NaturezaOperacao { get; set; }
        public string OptanteSimplesNacional { get; set; }
        public string IncentivadorCultural { get; set; }
        public string Status { get; set; }
        public RpsSubstituido RpsSubstituido { get; set; }
        public Servico Servico { get; set; }
        public Prestador Prestador { get; set; }
        public Tomador Tomador { get; set; }

        public InfRps()
        {
            IdentificacaoRps = new IdentificacaoRps();
            RpsSubstituido = new RpsSubstituido();
            Servico = new Servico();
            Prestador = new Prestador();
            Tomador = new Tomador();
        }
    }
}