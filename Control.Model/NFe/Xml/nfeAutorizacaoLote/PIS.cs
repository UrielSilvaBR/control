using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote
{
    public class PIS
    {
        public PISAliq PISAliq { get; set; }

        public PIS()
        {
            PISAliq = new PISAliq();
        }
    }
}