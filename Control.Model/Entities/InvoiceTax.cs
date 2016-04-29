using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    public class InvoiceTax : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        [ForeignKey("Id")]
        public virtual Invoice Invoice { get; set; }

        public decimal ValorIss { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorInss { get; set; }
        public decimal ValorIr { get; set; }
        public decimal ValorCsll { get; set; }
        public decimal Aliquota { get; set; }
    }
}
