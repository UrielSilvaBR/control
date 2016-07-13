using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class total
    {
        public ICMSTot ICMSTot { get; set; }

        public total()
        {
            ICMSTot = new ICMSTot();
        }
    }
}