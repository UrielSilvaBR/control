using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    public class Rps
    {
        public InfRps InfRps { get; set; }

        public Rps()
        {
            InfRps = new InfRps();
        }
    }
}
