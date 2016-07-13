using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.enviNFe
{
    public class NFe
    {
        public infNFe infNFe { get; set; }

        public NFe()
        {
            infNFe = new infNFe();
        }
    }
}