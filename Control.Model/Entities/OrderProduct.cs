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
    public class OrderProduct : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int SequencialItem { get; set; }

        [Display(Name ="Qtd.")]
        public decimal QuantityOrder { get; set; } 
        public decimal? QuantityDeliver { get; set; }
        [Display(Name = "Valor Unitário")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Desconto")]
        public decimal ItemDiscount { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Valor Total")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Prazo Entrega")]
        [Range(0, 1000)]
        public int DeadlineItem { get; set; }

        //Forgein Keys
        public int? ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product ProductItem { get; set; }

        public int? TypeUnitID { get; set; }
        [ForeignKey("TypeUnitID")]
        public virtual TypeUnit ProductUnit { get; set; }
    }
}
