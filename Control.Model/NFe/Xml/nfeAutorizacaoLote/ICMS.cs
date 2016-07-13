using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class ICMS
    {
        public ICMS00 ICMS00 { get; set; }

        public ICMS()
        {
            ICMS00 = new ICMS00();
        }
    }
}
