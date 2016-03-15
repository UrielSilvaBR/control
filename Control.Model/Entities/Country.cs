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
    public class Country : IEntity 
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
