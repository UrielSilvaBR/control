using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.enviNFe
{
    public class COFINS
    {
        public COFINSAliq COFINSAliq { get; set; }

        public COFINS()
        {
            COFINSAliq = new COFINSAliq();
        }
    }
}
