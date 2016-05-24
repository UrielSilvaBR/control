using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control.DAL.CEP.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control.Model.Entities;

namespace Control.DAL.CEP.Objects.Tests
{
    [TestClass()]
    public class CEPTests
    {
        [TestMethod()]
        public void GetCityByCEPTest()
        {
            var context = new DALContext();

            var StatesList = new List<State>();
            StatesList.Add(new State() { Name = "RONDÔNIA", UF = "RO", IBGECode = 11, CountryId = 1 });
            StatesList.Add(new State() { Name = "ACRE", UF = "AC", IBGECode =12 , CountryId = 1 });
            StatesList.Add(new State() { Name = "AMAZONAS", UF = "AM", IBGECode = 13, CountryId = 1 });
            StatesList.Add(new State() { Name = "RORAIMA", UF = "RR", IBGECode = 14, CountryId = 1 });
            StatesList.Add(new State() { Name = "PARÁ", UF = "PA", IBGECode = 15, CountryId = 1 });
            StatesList.Add(new State() { Name = "AMAPÁ", UF = "AP", IBGECode = 16, CountryId = 1 });
            StatesList.Add(new State() { Name = "TOCANTINS", UF = "TO", IBGECode = 17, CountryId = 1 });
            StatesList.Add(new State() { Name = "MARANHÃO", UF = "MA", IBGECode =21 , CountryId = 1 });
            StatesList.Add(new State() { Name = "PIAUÍ", UF = "PI", IBGECode = 22, CountryId = 1 });
            StatesList.Add(new State() { Name = "CEARÁ", UF = "CE", IBGECode = 23, CountryId = 1 });
            StatesList.Add(new State() { Name = "RIO GRANDE DO NORTE", UF = "RN", IBGECode = 24, CountryId = 1 });
            StatesList.Add(new State() { Name = "PARAÍBA", UF = "PB", IBGECode = 25, CountryId = 1 });
            StatesList.Add(new State() { Name = "PERNAMBUCO", UF = "PE", IBGECode = 26, CountryId = 1 });
            StatesList.Add(new State() { Name = "ALAGOAS", UF = "AL", IBGECode = 27, CountryId = 1 });
            StatesList.Add(new State() { Name = "SERGIPE", UF = "SE", IBGECode = 28, CountryId = 1 });
            StatesList.Add(new State() { Name = "BAHIA", UF = "BA", IBGECode = 29, CountryId = 1 });
            StatesList.Add(new State() { Name = "MINAS GERAIS", UF = "MG", IBGECode = 31, CountryId = 1 });
            StatesList.Add(new State() { Name = "ESPÍRITO SANTO", UF = "ES", IBGECode = 32, CountryId = 1 });
            StatesList.Add(new State() { Name = "RIO DE JANEIRO", UF = "RJ", IBGECode = 33, CountryId = 1 });
            StatesList.Add(new State() { Name = "SÃO PAULO", UF = "SP", IBGECode = 35, CountryId = 1 });
            StatesList.Add(new State() { Name = "PARANÁ", UF = "PR", IBGECode = 41, CountryId = 1 });
            StatesList.Add(new State() { Name = "SANTA CATARINA", UF = "SC", IBGECode = 42, CountryId = 1 });
            StatesList.Add(new State() { Name = "RIO GRANDE DO SUL", UF = "RS", IBGECode = 43, CountryId = 1 });
            StatesList.Add(new State() { Name = "MATO GROSSO DO SUL", UF = "MS", IBGECode = 50, CountryId = 1 });
            StatesList.Add(new State() { Name = "MATO GROSSO", UF = "MT", IBGECode = 51, CountryId = 1 });
            StatesList.Add(new State() { Name = "GOIÁS", UF = "GO", IBGECode = 52, CountryId = 1 });
            StatesList.Add(new State() { Name = "DISTRITO FEDERAL", UF = "DF", IBGECode = 53, CountryId = 1 });

            StatesList.ForEach(p =>
            {
                context.States.Create(p);
            });

            context.SaveChanges();

            Assert.Fail();
        }
    }
}