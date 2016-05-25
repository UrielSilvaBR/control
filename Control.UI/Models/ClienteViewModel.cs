using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control.UI.Models
{
    public class ClienteViewModel
    {
        private IDALContext context;

        public Customer Customer { get; set; }
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<State> States { get; set; }

        public ClienteViewModel()
        {
            context = new DALContext();
            Countries = context.Countries.All().OrderBy(p => p.Name).ToList();
            Cities = new List<City>();
            States = context.States.All().OrderBy(p => p.Name).ToList();
            States.Insert(0, new State() { Id = 0, Name = "SELECIONE..." });
        }
    }
}