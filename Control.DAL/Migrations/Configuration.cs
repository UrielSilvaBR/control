namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Control.Model.Entities;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<Control.DAL.Data.ControlContext>
    {
        public Configuration()
        {
           AutomaticMigrationsEnabled = true;
            //AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Control.DAL.Data.ControlContext context)
        {
            //http://www.ibge.gov.br/home/estatistica/populacao/registrocivil/2013/codigo_paises.shtm
            Country pais = new Country() { IBGECode = 076, Code = "BRA", Name = "Brasil" };

            context.Countries.Add(pais);

            IList<State> defaultStates = new List<State>();
            
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 12, UF = "AC", Name = "Acre" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 27, UF = "AL", Name = "Alagoas" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 13, UF = "AM", Name = "Amazonas" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 16, UF = "AP", Name = "Amapá" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 29, UF = "BA", Name = "Bahia" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 23, UF = "CE", Name = "Ceará" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 53, UF = "DF", Name = "Distrito Federal" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 32, UF = "ES", Name = "Espírito Santo" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 52, UF = "GO", Name = "Goiás" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 21, UF = "MA", Name = "Maranhão" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 31, UF = "MG", Name = "Minas Gerais" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 50, UF = "MS", Name = "Mato Grosso do Sul" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 51, UF = "MT", Name = "Mato Grosso" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 15, UF = "PA", Name = "Pará" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 25, UF = "PB", Name = "Paraíba" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 26, UF = "PE", Name = "Pernambuco" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 22, UF = "PI", Name = "Piauí" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 41, UF = "PR", Name = "Paraná" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 33, UF = "RJ", Name = "Rio de Janeiro" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 24, UF = "RN", Name = "Rio Grande do Norte" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 11, UF = "RO", Name = "Rondônia" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 14, UF = "RR", Name = "Roraima" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 43, UF = "RS", Name = "Rio Grande do Sul" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 42, UF = "SC", Name = "Santa Catarina" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 28, UF = "SE", Name = "Sergipe" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 35, UF = "SP", Name = "São Paulo" });
            defaultStates.Add(new State() { CountryId = 1, IBGECode = 17, UF = "TO", Name = "Tocantis" });

            foreach (State std in defaultStates)
                context.States.Add(std);

            base.Seed(context);
        }
    }
}
