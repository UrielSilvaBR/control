using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    [DataContract]
    public class PaymentTerm : IEntity
    {
        public PaymentTerm()
        {
            InsertDate = DateTime.Now;
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        [Display(Name ="Descrição")]
        public string Description { get; set; }

        [Display(Name = "Código")]
        public string ShortDescription { get; set; }

        [Display(Name = "Dias")]
        public int Days { get; set; }

        public DateTime InsertDate { get; set; }

        [Display(Name = "Ativa")]
        public bool IsActive { get; set; }
    }
}
