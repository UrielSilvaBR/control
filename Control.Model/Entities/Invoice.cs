using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    [DataContract]
    public class Invoice : IEntity
    {
        public Invoice()
        {
            InsertDate = DateTime.Now;
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required(ErrorMessage ="Número da Nota Fiscal é obrigatório!")]
        [Display(Name = "Número")]
        public Int64 Numero { get; set; }

        public int InvoiceSerieID { get; set; }
        [ForeignKey("InvoiceSerieID")]
        public virtual InvoiceSerie Serie { get; set; }

        [Required(ErrorMessage = "O valor da Nota Fiscal é obrigatório!")]
        public decimal Valor { get; set; }

        [Display(Name = "Emissão")]
        public DateTime DataEmissao { get; set; }

        public string Status { get; set; }

        [Display(Name = "Criação")]
        public DateTime InsertDate { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        ////Forgein Keys
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer CustomerInvoice { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual List<InvoiceItem> Items { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual List<InvoiceTax> Taxes { get; set; }
    }
}
