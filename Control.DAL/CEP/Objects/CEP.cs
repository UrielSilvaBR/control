using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Control.DAL.CEP.Objects
{
    public class CEP
    {
        public Model.Entities.City GetCityByCEP(string cep)
        {
            var servicoCidades = new servico.cidades.cidades();

            var tableCities = servicoCidades.RETORNA_CIDADE_CEP(cep).Tables[0];

            var City = tableCities.AsEnumerable()
                .Select(p => new Model.Entities.City()
                {
                    Name = p.Field<string>("NOMCID"),
                    CEPInicial = p.Field<int>("CPICID"),
                    CEPFinal = p.Field<int>("CPFCID"),
                    IBGECode = p.Field<int>("IBGCID"),
                    State = new Model.Entities.State() { UF = p.Field<string>("ESTCID"), IBGECode = p.Field<int>("IBGEST") }
                }).FirstOrDefault();

            return City;
        }
    }
}
