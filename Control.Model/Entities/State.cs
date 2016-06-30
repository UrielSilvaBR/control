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
    public class State : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UF { get; set; }

        [DataMember]
        public int IBGECode { get; set; }


        [NotMapped]
        public string StateName
        {
            get { return String.Format("{0} - {1}", UF, Name); }
        }
    }
}
