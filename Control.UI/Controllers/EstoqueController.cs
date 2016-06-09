using Control.DAL;
using Control.Model.Entities;
using Control.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Controllers
{
    public class EstoqueController : Controller
    {

        private IDALContext context;

        // GET: Estoque
        public ActionResult ConsultaEstoque()
        {
            context = new DALContext();
            List<Control.Model.Entities.Storage> retorno = new List<Control.Model.Entities.Storage>();
            retorno = context.Storages.All().ToList(); 
            return View(retorno);
        }

       
    }
}