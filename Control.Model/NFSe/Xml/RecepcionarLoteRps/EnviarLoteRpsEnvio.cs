using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Control.Model.NFSe.Xml.RecepcionarLoteRps
{
    [XmlRoot("EnviarLoteRpsEnvio", Namespace = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd"), Serializable]
    public class EnviarLoteRpsEnvio
    {
        [XmlElement("LoteRps")]
        public LoteRps LoteRps { get; set; }

        public EnviarLoteRpsEnvio()
        {
            LoteRps = new LoteRps();
        }
    }
}
