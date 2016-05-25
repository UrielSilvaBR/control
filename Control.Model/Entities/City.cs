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
    public class City : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int IBGECode { get; set; }

        [DataMember]
        public Int64 CEPInicial { get; set; }

        [DataMember]
        public Int64 CEPFinal { get; set; }

        [NotMapped]
        public Utility.CEP CEP { get; set; }
    }
}