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
        [Key]
        [DataMember]
        public int Id { get; set; }

        public string Status { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Comments { get; set; }
        public int CFOP { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalValue { get; set; }

        //Campos Nota Fiscal
        public int InvoiceNumber { get; set; }
        public int InvoiceStatus { get; set; }
        
        //Forgein Keys
        public int? CustomerID { get; set; }
        [ForeignKey("ClientID")]
        public virtual Customer CustomerOrder { get; set; }

        public int? OrderTypeID { get; set; }
        [ForeignKey("OrderTypeID")]
        public virtual OrderType TypeOrder { get; set; }

        public int? VendorID { get; set; }
        [ForeignKey("VendorID")]
        public virtual Vendor VendorOrder { get; set; }

        public int? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public virtual Contact ContactOrder { get; set; }
    }
}
