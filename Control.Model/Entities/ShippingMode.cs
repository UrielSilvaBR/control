using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Control.Model.Enums;

namespace Control.Model.Entities
{
    [DataContract]
    public class ShippingMode : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isWeightCharged { get; set; }
        public bool isVolumeCharged { get; set; }

        public decimal WeightPrice { get; set; }
        public decimal VolumePrice { get; set; }

        public string WeightUnit { get; set; }
        public string VolumeUnit { get; set; }


        public bool isClientCharged { get; set; }
        
    }
}
