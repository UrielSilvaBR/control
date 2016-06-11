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

        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Nota Fiscal")]
        public int? InvoiceNumber { get; set; }
        [Display(Name = "Data Cadastro")]
        public DateTime InsertDate { get; set; }
        [Display(Name = "Validade")]
        public DateTime ValidateDate { get; set; }
        [Display(Name = "Valor Total")]
        public decimal TotalValue { get; set; }
        [Display(Name = "Observação")]
        public string Comments { get; set; }
        
        [ForeignKey("PurchaseOrderId")]
        public virtual List<PurchaseOrderItem> Items { get; set; }


    }
}
