using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control.Model.Entities
{
    [DataContract]
    public class Order : IEntity
    {
        public Order()
        {
            InsertDate = DateTime.Now;
        }

        [Key]
        [DataMember]
        [Display(Name ="Proposta")]
        public int Id { get; set; }

        public string Status { get; set; }
        public DateTime InsertDate { get; set; }


        [Display(Name = "Emissão")]
        public DateTime OrderDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public string Comments { get; set; }
        public int? CFOP { get; set; }
        [Display(Name = "Desconto")]
        public decimal Discount { get; set; }
        [Display(Name = "Valor")]
        public decimal TotalValue { get; set; }

        //Campos Nota Fiscal
        public int? InvoiceNumber { get; set; }
        public int? InvoiceStatus { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderProduct> Items { get; set; }

        ////Forgein Keys
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer CustomerOrder { get; set; }

        public int OrderTypeID { get; set; }
        [ForeignKey("OrderTypeID")]
        public virtual OrderType TypeOrder { get; set; }

        public int VendorID { get; set; }
        [ForeignKey("VendorID")]
        public virtual Vendor VendorOrder { get; set; }

        public int ContactID { get; set; }
        [ForeignKey("ContactID")]
        public virtual Contact ContactOrder { get; set; }

        public int? PaymentTermID { get; set; }
        [ForeignKey("PaymentTermID")]
        public virtual PaymentTerm PaymentTerm { get; set; }
    }
}
