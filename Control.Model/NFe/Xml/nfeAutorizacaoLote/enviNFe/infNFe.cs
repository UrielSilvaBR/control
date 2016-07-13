using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFe.Xml.nfeAutorizacaoLote.enviNFe
{
    public class infNFe
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        [XmlAttribute("Id")]
        public string Id { get; set; }

        public ide ide { get; set; }
        public emit emit { get; set; }
        public dest dest { get; set; }

        [XmlElement("det")]
        public det[] det { get; set; }

        public total total { get; set; }
        public transp transp { get; set; }
        public pag pag { get; set; }
        public infAdic infAdic { get; set; }

        public infNFe()
        {
            ide = new ide();
            emit = new emit();
            dest = new dest();
            total = new total();
            transp = new transp();
            pag = new pag();
            infAdic = new infAdic();
        }

        public void GerarChaveAcessoNFe()
        {
            string chaveNFe = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                emit.enderEmit.UF.PadLeft(2, '0'),
                ide.dhEmi.ToString("yyMM"),
                emit.CNPJ.PadLeft(14, '0'),
                ide.mod.PadLeft(2, '0'),
                ide.serie.PadLeft(3, '0'),
                ide.nNF.PadLeft(9, '0'),
                ide.tpEmis,
                ide.cNF.PadLeft(8, '0'));

            chaveNFe = String.Format("NFe{0}{1}", chaveNFe, CalculoDV(chaveNFe));

            Id = chaveNFe;
        }

        private string CalculoDV(string chaveNFe)
        {
            string chaveInvertida = Utility.Utilities.ReverseString(chaveNFe);
            int[] t = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int somatorio = 0;
            int posicaoParaCalculo = 0;
            foreach (var v in chaveInvertida)
            {
                somatorio = somatorio + (int.Parse(v.ToString()) * t[posicaoParaCalculo]);
                if (posicaoParaCalculo == 7)
                {
                    posicaoParaCalculo = 0;
                }
                else
                {
                    posicaoParaCalculo += 1;
                }
            }

            int resto = somatorio % 11;
            int dv;
            if (resto == 0 || resto == 1)
            {
                dv = 0;
            }
            else
            {
                dv = (11 - resto);
            }

            return dv.ToString();
        }
    }
}