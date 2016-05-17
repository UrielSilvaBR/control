using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Models
{
    public class MovimentacaoEstoqueViewModel
    {

         
        public SelectList Produtos { get; set; }

        public Storage EstoqueEditar { get; set; }
        
        public List<Storage> Lista { get; set; }
    }
}