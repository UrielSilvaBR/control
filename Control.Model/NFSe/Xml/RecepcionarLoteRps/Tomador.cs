using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    public class Tomador
    {
        public IdentificacaoTomador IdentificacaoTomador { get; set; }
        public string RazaoSocial { get; set; }
        public Endereco Endereco { get; set; }

        public Tomador()
        {
            IdentificacaoTomador = new IdentificacaoTomador();
            Endereco = new Endereco();
        }
    }
}
