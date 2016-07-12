using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class imposto
    {
        public ICMS ICMS { get; set; }
        public IPI IPI { get; set; }
        public PIS PIS { get; set; }
        public COFINS COFINS { get; set; }

        public imposto()
        {
            ICMS = new ICMS();
            IPI = new IPI();
            PIS = new PIS();
            COFINS = new COFINS();
        }
    }
}
