using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class dest
    {
        public string CNPJ { get; set; }
        public string xNome { get; set; }

        public enderDest enderDest { get; set; }

        public string indIEDest { get; set; }
        public string IE { get; set; }
        public string email { get; set; }

        public dest()
        {
            enderDest = new enderDest();
        }
    }
}