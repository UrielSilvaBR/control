using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class transp
    {
        public string modFrete { get; set; }

        public transporta transporta { get; set; }
        public vol vol { get; set; }

        public transp()
        {
            transporta = new transporta();
            vol = new vol();
        }
    }
}
