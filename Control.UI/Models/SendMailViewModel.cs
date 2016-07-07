using Control.DAL;
using Control.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control.UI.Models
{
    public class SendMailViewModel
    {
        private IDALContext context;

        public Model.Entities.Order Order { get; set; }
        public Model.Entities.OrderProduct OrderProduct { get; set; }
        public List<UserAdressBook> ListaEmails { get; set; }
        public string LoggerUser { get; set; }
        public string NovoEmail { get; set; }
        public string Assunto { get; set; }

        public SendMailViewModel()
        {
             ListaEmails = new List<UserAdressBook>();
            context = new DALContext();
            try
            {
                if (string.IsNullOrEmpty(LoggerUser))
                {
                    ListaEmails = context.AddressBooks.All().ToList();
                }
                else
                {
                    ListaEmails = context.AddressBooks.All().Where(p => p.Username.ToUpper().Contains(LoggerUser)).ToList();
                }

                if (Order == null)
                {
                    Order = new Order();
                    Order.Items = new List<OrderProduct>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLoggedUser()
        {
            ListaEmails = context.AddressBooks.All().Where(p => p.Username.ToUpper().Contains(LoggerUser)).ToList();
        }

        public void AddEmailAddress(string Email)
        {
            if (string.IsNullOrEmpty(Order.ProposalMailList))
            {
                Order.ProposalMailList = Email;
            }
            else
                Order.ProposalMailList = Order.ProposalMailList + "; " + Email;
        }
    }
}