﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    [DataContract]
    public class InvoiceSerie : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public string Descricao { get; set; }

    }
}
