﻿using System;
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
    public class InvoiceItem : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public int SequencialItem { get; set; }
        public decimal QuantityOrder { get; set; }
        public decimal QuantityDeliver { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ItemDiscount { get; set; }
        public string Comments { get; set; }
        public decimal TotalPrice { get; set; }

        //Forgein Keys
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product ProductItem { get; set; }
    }
}
