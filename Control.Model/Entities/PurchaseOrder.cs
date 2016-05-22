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
    [DataContract]
   public class PurchaseOrder : IEntity
    {
        [Key]
        [DataMember] 
        public int Id { get; set; }
         
        public int? ProviderID { get; set; }

        [ForeignKey("ProviderID")]
        public virtual Provider ProviderPurchaseOrder { get; set; }

        public string Status { get; set; }

        public int? InvoiceNumber { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime ValidateDate { get; set; }

        public decimal TotalValue { get; set; }

        public string Comments { get; set; }
        
        [ForeignKey("PurchaseOrderId")]
        public virtual List<PurchaseOrderItem> Items { get; set; }


    }
}
