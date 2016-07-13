using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class IPI
    {
        public string cEnq { get; set; }
        public IPINT IPINT { get; set; }

        public IPI()
        {
            IPINT = new IPINT();
        }
    }
}
