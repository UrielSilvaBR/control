using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.enviNFe
{
    public class emit
    {
        public string CNPJ { get; set; }
        public string xNome { get; set; }
        public string xFant { get; set; }
        public enderEmit enderEmit { get; set; }
        public string IE { get; set; }
        public string IM { get; set; }
        public string CNAE { get; set; }
        public string CRT { get; set; }

        public emit()
        {
            enderEmit = new enderEmit();
        }
    }
}
