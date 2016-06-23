using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Control.Model.Enums;

namespace Control.Model.Entities
{
    [DataContract]
    public class UserAdressBook : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        public string Username { get; set; }
        public string MailContactDescription { get; set; }
        public string MailContactName { get; set; }
        public string MailContactEmai { get; set; }
    }
}
