using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    public class Servico
    {
        public Valores Valores { get; set; }
        public string ItemListaServico { get; set; }
        public string CodigoTributacaoMunicipio { get; set; }
        public string Discriminacao { get; set; }
        public string CodigoMunicipio { get; set; }


        public Servico()
        {
            Valores = new Valores();
        }
    }
}
