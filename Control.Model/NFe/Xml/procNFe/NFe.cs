﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.procNFe
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
