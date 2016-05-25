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
    public class PurchaseOrderItem : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; } 

        public int PurchaseOrderId { get; set; }
                   
        public int SequencialItem { get; set; }

        [Display(Name = "Quantidade")]
        public decimal QuantityOrder { get; set; }

        [Display(Name = "Qtd. Entregue")]
        public decimal QuantityDeliver { get; set; }
        [Display(Name = "Valor Unitário")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Desconto")]
        public decimal ItemDiscount { get; set; }

        [Display(Name = "Valor Total")]
        public decimal TotalPrice { get; set; }

        //Forgein Keys
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product ProductItem { get; set; }
    }
}
